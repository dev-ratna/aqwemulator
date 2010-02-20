using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Net;
using AQWE.Game.Managers;

namespace AQWE.Game.Rooms
{
    public class Room
    {
        internal int ID; //You have joined battleon-XXX
        internal int mapID;
        internal string mapName;
        internal string fileName;
        public Dictionary<int, userManager> activeUsers;

        public Room(int mapID)
        {
            this.mapID = mapID;
            activeUsers = new Dictionary<int, userManager>();

            roomManager.Add(this.mapID, this);
        }

        public userManager getUserIntsance(int userID)
        {
            if (activeUsers.ContainsKey(userID))
                return (userManager)activeUsers[userID];
            else
                throw new Exception("User ID: " + userID + " doesn't exist");
        }

        public userManager getPlayerIntsance(int _playerID)
        {
            foreach (KeyValuePair<int, userManager> KVP in activeUsers)
            {
                if (KVP.Value.Session.userInfo.playerID == _playerID)
                {
                    return (userManager)KVP.Value;
                }
            }

            throw new Exception("No player data found.");
        }

        public void AddUser(int userID, userManager User)
        {
            if (!activeUsers.ContainsKey(userID))
                activeUsers.Add(userID, User);
        }

        public void RemoveUser(int userID)
        {
            if (activeUsers.ContainsKey(userID))
                activeUsers.Remove(userID);
        }

        public int CountUsers()
        {
            return activeUsers.Count;
        }
    }
}
