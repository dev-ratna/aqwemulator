using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

using AQWE.Core;
using AQWE.Sessions;
using AQWE.Game.Rooms;
using AQWE.Game.Users;
using AQWE.Game.Managers;

namespace AQWE.Net
{
    public class Packets
    {
        internal userManager Connection;

        public Packets(userManager Connection)
        {
            this.Connection = Connection;
        }

        public void Handle(string Message)
        {
            Logging.logClientMessage(this.Connection.connectionID, Message);

            if (Message.Contains("<policy") || Message.Contains("<msg"))
                this.handleXML(Message);
            else
            {
                string[] packet = Message.Split('%');
                switch (packet[1])
                {
                    case "xt":
                        switch (packet[2])
                        {
                            case "zm":
                                switch (packet[3])
                                {
                                    case "firstJoin":
                                        handleJoin(packet);
                                        break;
                                    case "moveToCell":
                                        Connection.Session.userInfo.Frame = packet[5];
                                        Connection.Session.userInfo.Pad = packet[6];
                                        break;
                                    case "retrieveUserDatas":
                                        handleRetrieveUserDatas(packet);
                                        break;
                                    case "cmd":
                                        switch (packet[5])
                                        {
                                            case "ignoreList":
                                                switch (packet[6])
                                                {
                                                    case "clearAll":
                                                        //this.Connection.sendMessage("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"cvu\",\"o\":{\"PCstRatio\":3.73,\"PChpDelta\":1640,\"baseHit\":0,\"PChpBase1\":360,\"resistRating\":17,\"baseCritValue\":1.7,\"PChpGoal100\":4000,\"intLevelCap\":100,\"baseMiss\":0.06,\"baseParry\":0.03,\"GstBase\":6,\"modRating\":3,\"baseResistValue\":0.7,\"baseBlockValue\":0.7,\"intHPperEND\":10,\"baseHaste\":0,\"baseBlock\":0,\"PChpBase100\":2000,\"intAPtoDPS\":20,\"baseCrit\":0.05,\"PCstBase\":8,\"baseEventValue\":0.05,\"PChpGoal1\":400,\"GstRatio\":2.8,\"intLevelMax\":100,\"bigNumberBase\":8,\"baseDodge\":0.04,\"PCDPSMod\":0.85}}}}");
                                                        break;
                                                    default:
                                                        Logging.logWarning("Unknown packet structure: " + packet[5] + ". In: " + Message + ".");
                                                        break;
                                                }
                                                break;
                                            default:
                                                Logging.logWarning("Unknown packet structure: " + packet[4] + ". In: " + Message + ".");
                                                break;
                                        }
                                        break;
                                    default:
                                        Logging.logWarning("Unknown packet structure: " + packet[3] + ". In: " + Message + ".");
                                        break;
                                }
                                break;
                            default:
                                Logging.logWarning("Unknown packet structure: " + packet[2] + ". In: " + Message + ".");
                                break;
                        }
                        break;
                    default:
                        Logging.logWarning("Unknown packet structure: " + packet[1] + ". In: " + Message + ".");
                        break;
                }
            }
        }

        public void handleRetrieveUserDatas(string[] Packets)
        {
            int roomID = int.Parse(Packets[4]);
            Room _room = (Room)roomManager.getInstance(roomManager.getClientRoomID(roomID));

            int pL = Packets.Length;
            string returnPacket = "{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"initUserDatas\",\"a\":[";

            if (pL > 5)
            {
                for (int i = 5; i < pL; i++)
                {
                    if (i != 5)
                        returnPacket += ",";

                    userManager _user = _room.getPlayerIntsance(int.Parse(Packets[i]));
                    User _userInfo = _user.Session.userInfo;
                    returnPacket = "{\"uid\":" + _userInfo.userID + ",\"strFrame\":\"" + _userInfo.Frame + "\",\"strPad\":\"" + _userInfo.Pad + "\",\"data\":{\"intColorAccessory\":\"" + _userInfo.ColorAccessory + "\",\"iCP\":0,\"intLevel\":\"" + _userInfo.Level + "\",\"iBagSlots\":" + _userInfo.BagSlots + ",\"ig0\":0,\"iUpgDays\":\"" + _userInfo.UpgradeDays + "\",\"intColorBase\":\"" + _userInfo.ColorBase + "\",\"sCountry\":\"EE\",\"iSTR\":\"0\",\"ip0\":0,\"iq0\":0,\"iAge\":\"13\",\"iWIS\":\"0\",\"intExpToLevel\":\"1000000\",\"intGold\":1201076,\"intMP\":" + _userInfo.MP + ",\"sHouseInfo\":[],\"iBankSlots\":0,\"iHouseSlots\":20,\"id0\":0,\"intColorSkin\":\"15388042\",\"intMPMax\":40,\"intHPMax\":500,\"dUpgExp\":\"2008-10-09T16:33:00\",\"iUpg\":\"1\",\"CharID\":\"" + _userInfo.userID + "\",\"strEmail\":\"en3rgyx@gmail.com\",\"iINT\":\"0\",\"intColorTrim\":\"5398908\",\"lastArea\":\"battleon-7\",\"iFounder\":\"0\",\"intDBExp\":0,\"intExp\":0,\"UserID\":\"" + _userInfo.userID + "\",\"ia1\":\"0\",\"ia0\":2,\"intHP\":" + _userInfo.HP + ",\"dCreated\":\"2008-10-10T16:33:00\",\"strQuests\":\"70000000000000000000004000000000000000001200000000\",\"bitSuccess\":\"1\",\"strHairName\":\"Saf1\",\"intColorEye\":\"26367\",\"iLCK\":\"0\",\"eqp\":{},\"iDailyAds\":0,\"iDBCP\":0,\"intDBGold\":1201076,\"intActivationFlag\":\"1\",\"intAccessLevel\":\"" + _userInfo.Access + "\",\"strHairFilename\":\"hair/M/Default.swf\",\"intColorHair\":\"65793\",\"HairID\":\"1\",\"strGender\":\"M\",\"strUsername\":\"" + _userInfo.Username + "\",\"iDEX\":\"0\",\"iDailyAdCap\":6,\"intCoins\":0,\"iEND\":\"0\",\"strMapName\":\"" + _room.mapName + "\"}}";
                }
            }

            returnPacket += "]}}}";
            Connection.sendMessage(returnPacket);
        }

        public void handleJoin(string[] Message)
        {
            string returnPacket = "";

            try
            {
                int _roomID = roomManager.Open(Settings.map_first_join, Connection.Session.userInfo.userID);
                
                Room _room = roomManager.getInstance(_roomID);
                _room.AddUser(Connection.Session.userInfo.userID, Connection);

                int _playerID = _room.CountUsers();

                Connection.Session.userInfo.roomID = _roomID;
                Connection.Session.userInfo.playerID = _playerID;
                
                returnPacket = "<msg t='sys'><body action='joinOK' r='" + roomManager.getGameRoomID(_roomID) + "'><pid id='" + _playerID + "'/><vars />";
                returnPacket += roomManager.getPlayers(_roomID);
                returnPacket += "</body></msg>";
                
                this.Connection.sendMessage(returnPacket);
                this.Connection.sendMessage(roomManager.getMoveToArea(_roomID));
                this.Connection.sendMessage("%xt%server%-1%You joined \"" + _room.mapName + "-" + _roomID + "\"!%");

                roomManager.UserJoined(Connection, _roomID);
            }
            catch (Exception ex)
            {
                Logging.logError("Error while [" + Connection.Session.userInfo.Username + "] were trying to join. " + ex.Message);
            }
        }

        public void handleXML(string Message)
        {
            if (Message.Contains("<policy"))
            {
                this.Connection.sendMessage("<cross-domain-policy><allow-access-from domain='*' to-ports='" + Settings.server_port + "' /></cross-domain-policy>");
            }
            else if (Message.Contains("verChk"))
            {
                //this.Connection.sendMessage("<cross-domain-policy><allow-access-from domain='*' to-ports='" + Settings.server_port + "' /></cross-domain-policy>");
                this.Connection.sendMessage("<msg t='sys'><body action='apiOK' r='0'></body></msg>");
            }
            else if (Message.Contains("login"))
            {
                try
                {
                    string tUsername = "";
                    string tPassword = "";
                    XmlDocument _xdoc = new XmlDocument();
                    XmlNodeList _xnl;
                    _xdoc.LoadXml(Message);

                    // Username
                    _xnl = _xdoc.SelectNodes("/msg/body/login/nick");
                    foreach (XmlNode _xn in _xnl)
                    {
                        tUsername += _xn.InnerText;
                    }

                    // Password
                    _xnl = _xdoc.SelectNodes("/msg/body/login/pword");
                    foreach (XmlNode _xn in _xnl)
                    {
                        tPassword += _xn.InnerText;
                    }

                    sessionManager.attemptLogin(tUsername, tPassword, this);
                }
                catch (Exception ex)
                {
                    Logging.logError(ex.Message);
                }
            }
        }
    }
}
