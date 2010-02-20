using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Core;
using AQWE.Game.Rooms;
using AQWE.Game.Users;

namespace AQWE.Game.Managers
{
    public static class roomManager
    {
        public static Dictionary<int, Room> Rooms = new Dictionary<int, Room>();

        public static Room getInstance(int roomID)
        {
            if (Rooms.ContainsKey(roomID))
                return (Room)Rooms[roomID];
            else
                throw new Exception("Room id: " + roomID + " doesn't exist.");
        }

        /// <summary>
        /// Adds a new room to the room manager.
        /// </summary>
        /// <param name="roomID">The id of the room.</param>
        /// <param name="_Room">The object of the room.</param>
        public static void Add(int roomID, Room _Room)
        {
            if (!Rooms.ContainsKey(roomID))
            {
                Rooms.Add(roomID, _Room);
                Logging.logInfo("Room [" + roomID + "] loaded.");
            }
        }

        /// <summary>
        /// Removes a room from the room manager.
        /// </summary>
        /// <param name="roomID">The id of the room to remove.</param>
        public static void Remove(int roomID)
        {
            if (Rooms.ContainsKey(roomID))
            {
                Rooms.Remove(roomID);
                Logging.logInfo("Room [" + roomID + "] has been terminated.");
            }
        }

        public static int Open(int mapID, int userID)
        {
            Map _map = mapManager.getInstance(mapID);
            int tempRoomID = FindEmptyRoom(_map.ID);

            if (tempRoomID == 0)
            {
                Room _room = new Room(_map.ID);

                _room.mapID     = _map.ID;
                _room.mapName   = _map.Name;
                _room.fileName  = _map.Filename;
                _room.ID        = Rooms.Count;

                return _room.ID;
            }
            else
            {
                return tempRoomID;
            }

        }

        public static void UserJoined(userManager _user, int _roomID)
        {
            Room _room = getInstance(_roomID);
            User _userInfo = _user.Session.userInfo;
            int tRoomID = getGameRoomID(_roomID);

            sendToRoom(_roomID, "<msg t='sys'><body action='uER' r='" + tRoomID + "'><u i='" + _userInfo.userID + "' m='" + _user.isModerator + "' s='0' p='" + _userInfo.playerID + "'><n><![CDATA[" + _userInfo.Username + "]]></n><vars></vars></u></body></msg>");
            sendToRoom(_roomID, "%xt%uotls%-1%" + _userInfo.Username + "%afk:" + _user.isAFK + ",intHP:" + _userInfo.HP + ",strPad:" + _userInfo.Pad + ",intMPMax:" + _userInfo.MaxMP + ",uoName:" + _userInfo.Username + ",tx:" + _userInfo.X + ",ty:" + _userInfo.Y + ",intState:" + _userInfo.State + ",intLevel:" + _userInfo.Level + ",strUsername:" + _userInfo.Username + ",intHPMax:" + _userInfo.MaxHP + ",intMP:" + _userInfo.MP + ",strFrame:" + _userInfo.Frame + "%");
        }

        public static void UserLeft(userManager _user, int _roomID)
        {
            int tRoomID = getGameRoomID(_roomID);
            sendToRoom(_roomID, "<msg t='sys'><body action='userGone' r='" + tRoomID + "'><user id='" + _user.Session.userInfo.userID + "' /></body></msg>");
            sendToRoom(_roomID, "%xt%exitArea%-1%" + _user.Session.userInfo.userID + "%");
        }

        public static string getPlayers(int _roomID)
        {
            int tRoomID = getGameRoomID(_roomID);
            string ret = "<uLs r='" + tRoomID + "'>";

            Room _room = getInstance(_roomID);
            foreach (KeyValuePair<int, userManager> KVP in _room.activeUsers)
            {
                userManager _user = KVP.Value;
                User _userInfo = _user.Session.userInfo;

                ret += "<u i='" + _userInfo.userID + "' m='" + _user.isModerator + "' s='0' p='" + _userInfo.playerID + "'><n><![CDATA[" + _userInfo.Username + "]]></n><vars></vars></u>";
            }

            ret += "</uLs>";
            return ret;
        }

        public static string getMoveToArea(int _roomID)
        {
            Room _room = getInstance(_roomID);
            int tRoomID = getGameRoomID(_roomID);

            string ret = "{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"moveToArea\",\"areaName\":\"" + _room.mapName + "-" + _room.ID + "\",\"intKillCount\":0,\"uoBranch\":";
            ret += getUoBranch(_roomID);
            ret += ",\"strMapFileName\":\"" + _room.fileName + "\",\"intType\":\"2\",\"monBranch\":[],\"wB\":[],\"sExtra\":\"a1=test1,a2=test2\",\"areaId\":" + tRoomID + ",\"strMapName\":\"" + _room.mapName + "\"}}}";
            
            return ret;
        }

        public static string getUoBranch(int _roomID)
        {
            string ret = "[";
            bool firstTime = true;
            Room _room = getInstance(_roomID);

            foreach(KeyValuePair<int, userManager> KVP in _room.activeUsers)
            {
                userManager _user = KVP.Value;
                User _userInfo = _user.Session.userInfo;

                if (!firstTime)
                    ret += ",";

                ret += "[\"uoName:" + _userInfo.Username + "\",\"strUsername:" + _userInfo.Username + "\",\"strFrame:" + _userInfo.Frame + "\",\"strPad:" + _userInfo.Pad + "\",\"intState:" + _userInfo.State + "\",\"intLevel:" + _userInfo.Level + "\",\"intHP:" + _userInfo.HP + "\",\"intMP:" + _userInfo.MP + "\",\"intHPMax:" + _userInfo.MaxHP + "\",\"intMPMax:" + _userInfo.MaxMP + "\",\"tx:" + _userInfo.X + "\",\"ty:" + _userInfo.Y + "\",\"afk:" + _user.isAFK + "\"]";

                firstTime = false;
            }
            ret += "]";

            return ret;
        }

        public static void sendToRoom(int _roomID, string Data)
        {
            Room _room = getInstance(_roomID);
            foreach (KeyValuePair<int, userManager> KVP in _room.activeUsers)
            {
                KVP.Value.sendMessage(Data);
            }
        }

        public static int FindEmptyRoom(int mapID)
        {
            foreach (KeyValuePair<int, Room> KVP in Rooms)
            {
                if (KVP.Value.mapID == mapID && KVP.Value.CountUsers() < Settings.room_user_limit)
                    return KVP.Value.ID;
            }

            return 0;
        }

        public static int getGameRoomID(int _roomID)
        {
            return _roomID + 1; //return _roomID + 4;
        }
    }
}
