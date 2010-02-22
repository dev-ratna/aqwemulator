using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

using AQWE.Core;
using AQWE.Game;
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
                            case "exitArea":
                                break;
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
                                    case "retrieveUserData":
                                        handleRetrieveUserData(packet);
                                        break;
                                    case "retrieveInventory":
                                        handleRetrieveInventory(packet);
                                        break;
                                    case "mv":
                                        Room _room = roomManager.getInstance(roomManager.getClientRoomID(int.Parse(packet[4])));
                                        User _userInfo = Connection.Session.userInfo;

                                        _userInfo.X = int.Parse(packet[5]);
                                        _userInfo.Y = int.Parse(packet[6]);

                                        roomManager.sendToRoom(_room.ID, "%xt%uotls%-1%" + _userInfo.Username + "%,afk:false,sp:" + _userInfo.Speed + ",tx:" + _userInfo.X + ",ty:" + _userInfo.Y + ",strFrame:" + _userInfo.Frame + "%");
                                        break;
                                    case "afk":
                                        handleAFK(bool.Parse(packet[5]));
                                        break;
                                    case "cc":
                                        handleCannedChat(packet[5]);
                                        break;
                                    case "emotea":
                                        handleEmotea(packet[5]);
                                        break;
                                    case "cmd":
                                        switch (packet[5])
                                        {
                                            case "ignoreList":
                                                switch (packet[6])
                                                {
                                                    case "$clearAll":
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

        public void handleRetrieveInventory(string[] Packet)
        {
            int _roomID = int.Parse(Packet[4]);
            int _playerID = int.Parse(Packet[5]);

            Connection.sendMessage("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"aura-\",\"auras\":[],\"tInf\":\"p:" + _playerID + "\"}}}");
            Connection.sendMessage("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"sAct\",\"actions\":{\"passive\":[{\"icon\":\"iwd1\",\"ref\":\"p1\",\"nam\":\"Furious\",\"desc\":\"Increases all damage done by 10%\",\"auras\":[{\"nam\":\"Furious\",\"e\":[{\"val\":0.1,\"sta\":\"_ao\",\"typ\":\"mb\"}]}],\"typ\":\"pa\"},{\"icon\":\"iwd1\",\"ref\":\"p2\",\"nam\":\"Relentless\",\"desc\":\"(NYI) Chance to deal extra attack on normal swing.\",\"auras\":[{\"val\":0.05,\"nam\":\"Relentless\"}],\"typ\":\"pa\"}],\"active\":[{\"icon\":\"iwd1\",\"nam\":\"Auto attack\",\"anim\":\"Attack1,Attack2\",\"desc\":\"A strong attack known only to disciplined fighters.\",\"range\":301,\"fx\":\"m\",\"damage\":1.1,\"mana\":0,\"dsrc\":\"wDMG\",\"ref\":\"aa\",\"auto\":true,\"tgt\":\"h\",\"typ\":\"aa\",\"strl\":\"\",\"cd\":2000},{\"icon\":\"imr1,iwaxe\",\"nam\":\"Breaker\",\"anim\":\"Attack3\",\"desc\":\"Deals weapon DPS as damage and reduces opponents damage output by 50% for 2 actions\",\"auras\":[{\"nam\":\"Breaker\",\"t\":\"a\",\"e\":[{\"val\":0.5,\"sta\":\"_ao\",\"typ\":\"mr\"}],\"dur\":2,\"tgt\":\"h\"}],\"range\":301,\"fx\":\"m\",\"damage\":1,\"mana\":6,\"dsrc\":\"wDPS\",\"ref\":\"a1\",\"tgt\":\"h\",\"typ\":\"p\",\"strl\":\"\",\"cd\":8000},{\"icon\":\"ims2,iwsword\",\"nam\":\"Broadside\",\"anim\":\"Attack2\",\"desc\":\"(NYI) Deals 70% of weapon damage, cannot be avoided but also cannot crit.\",\"range\":301,\"fx\":\"m\",\"damage\":0.7,\"mana\":4,\"dsrc\":\"wDMG\",\"ref\":\"a2\",\"tgt\":\"h\",\"typ\":\"p\",\"cd\":4000},{\"icon\":\"iss1\",\"nam\":\"Lunge\",\"anim\":\"Stab\",\"desc\":\"Deals very light damage, and cancels opponent's next action\",\"auras\":[{\"cat\":\"disable\",\"nam\":\"Disabled\",\"t\":\"a\",\"dur\":1,\"s\":\"s\",\"tgt\":\"h\"}],\"range\":301,\"fx\":\"m\",\"damage\":0.1,\"mana\":8,\"dsrc\":\"wDMG\",\"ref\":\"a3\",\"tgt\":\"h\",\"typ\":\"p\",\"cd\":10000},{\"icon\":\"ied1\",\"nam\":\"Forgone Conclusion\",\"anim\":\"Point\",\"desc\":\"(NYI) Increases attack speed by 50% for 5 Attacks.  If opponent dies within 5 attacks, 2AP are gained for each attack that landed while under the effects of Forgone Conclusion.\",\"range\":301,\"auras\":[{\"val\":2,\"nam\":\"Forgone Conclusion\",\"t\":\"a\",\"e\":[{\"val\":0.5,\"sta\":\"_haste\",\"typ\":\"mb\"}],\"dur\":5,\"tgt\":\"s\"}],\"fx\":\"m\",\"mana\":10,\"ref\":\"a4\",\"tgt\":\"s\",\"typ\":\"p\",\"strl\":\"\",\"cd\":30000}]}}}}");
            Connection.sendMessage("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"stu\",\"data\":{\"_lck\":4,\"_pi\":1,\"_di\":1,\"$haste\":0,\"_po\":1,\"_mri\":0.01,\"$hi\":1,\"$ho\":1,\"$ao\":1.1,\"_ho\":1,\"_mro\":0.01,\"$mra\":2,\"_do\":1,\"$hit\":0,\"_end\":19,\"_hi\":1,\"_mrr\":5,\"$po\":1,\"$cha\":6,\"$end\":19,\"$ai\":1,\"_mo\":1,\"_str\":17,\"_hit\":0,\"_dodge\":0.05,\"_haste\":0,\"_cha\":6,\"$crit\":0.25,\"_mi\":1,\"$dex\":14,\"_int\":3,\"$mo\":1,\"$dodge\":0.05,\"_dex\":14,\"$mi\":1,\"$pi\":1,\"$lck\":4,\"$str\":17,\"$mro\":0.01,\"$mri\":0.01,\"_ao\":1,\"_crit\":0.25,\"$int\":3,\"$do\":1,\"_ai\":1,\"$mrr\":5,\"$di\":1,\"_mra\":2}}}}");
            Connection.sendMessage("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"loadInventoryBig\",\"friends\":[]}}}");
            Connection.sendMessage("%xt%server%-1%Character load complete.%");
        }

        public void handleEmotea(string Emote)
        {
            Room _room = roomManager.getInstance(Connection.Session.userInfo.roomID);
            roomManager.sendToRoom(_room.ID, "%xt%emotea%-1%" + Emote + "%" + Connection.Session.userInfo.Username + "%");
        }

        public void handleCannedChat(string Message)
        {
            Room _room = roomManager.getInstance(Connection.Session.userInfo.roomID);
            roomManager.sendToRoom(_room.ID, "%xt%cc%-1%" + Message + "%" + Connection.Session.userInfo.Username + "%");
        }

        public void handleAFK(bool state)
        {
            Room _room = roomManager.getInstance(Connection.Session.userInfo.roomID);

            Connection.Session.userInfo.Afk = state;
            User _userInfo = Connection.Session.userInfo;

            if (Connection.Session.userInfo.Afk)
                Connection.sendMessage("%xt%server%-1%You are now AFK (Away From Keyboard).%");
            else
                Connection.sendMessage("%xt%server%-1%You are no longer AFK (Away From Keyboard).%");

            roomManager.sendToRoom(_room.ID, "%xt%uotls%-1%" + _userInfo.Username + "%afk:" + state.ToString() + ",uoName:" + _userInfo.Username.ToLower() + ",tx:" + _userInfo.X + ",ty:" + _userInfo.Y + ",intState:" + _userInfo.State + "%");
            
        }

        public void handleRetrieveUserDatas(string[] Packets)
        {
            try
            {
                int roomID = int.Parse(Packets[4]);
                Room _room = (Room)roomManager.getInstance(roomManager.getClientRoomID(roomID));

                int pL = Packets.Length;
                string returnPacket = "{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"initUserDatas\",\"a\":[";

                if (pL > 4)
                {
                    for (int i = 5; i < pL - 1; i++)
                    {
                        if (i != 5)
                            returnPacket += ",";

                        userManager _user = (userManager)_room.getUserInstance(int.Parse(Packets[i]));
                        User _userInfo = (User)_user.Session.userInfo;
                        Hair _userHair = (Hair)hairManager.getInstance(_userInfo.HairID);

                        returnPacket += "{\"uid\":" + _userInfo.userID + ",\"strFrame\":\"" + _userInfo.Frame + "\",\"strPad\":\"" + _userInfo.Pad + "\",\"data\":{\"intColorAccessory\":\"" + _userInfo.ColorAccessory + "\",\"intColorTrim\":\"" + _userInfo.ColorTrim + "\",\"intMP\":" + _userInfo.MP + ",\"intLevel\":\"" + _userInfo.Level + "\",\"intColorSkin\":\"" + _userInfo.ColorSkin + "\",\"intMPMax\":" + _userInfo.MaxMP + ",\"intAccessLevel\":\"" + _userInfo.Access + "\",\"intHP\":" + _userInfo.HP + ",\"intColorBase\":\"" + _userInfo.ColorBase + "\",\"strHairFilename\":\"" + _userHair.Filename + "\",\"intHPMax\":" + _userInfo.MaxHP + ",\"intColorHair\":\"" + _userInfo.ColorHair + "\",\"HairID\":\"" + _userInfo.HairID + "\",\"intColorEye\":\"" + _userInfo.ColorEye + "\",\"strHairName\":\"" + _userHair.Name + "\",\"strGender\":\"" + _userInfo.Gender + "\",\"strUsername\":\"" + _userInfo.Username + "\",\"strClassName\":\"" + _userInfo.className + "\",\"eqp\":{" + userItemManager.getEquippedItems(_userInfo.userID) + "}}}";
                    }
                }

                returnPacket += "]}}}";
                Connection.sendMessage(returnPacket);
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message);
            }
        }

        public void handleRetrieveUserData(string[] Packets)
        {
            try
            {
                int roomID = int.Parse(Packets[4]);
                Room _room = (Room)roomManager.getInstance(roomManager.getClientRoomID(roomID));

                userManager _user = (userManager)_room.getUserInstance(int.Parse(Packets[5]));
                User _userInfo = (User)_user.Session.userInfo;
                Hair _userHair = (Hair)hairManager.getInstance(_userInfo.HairID);

                string returnPacket = "{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"uid\":" + _userInfo.userID + ",\"strFrame\":\"" + _userInfo.Frame + "\",\"cmd\":\"initUserData\",\"strPad\":\"" + _userInfo.Pad + "\",\"data\":{\"intColorAccessory\":\"" + _userInfo.ColorAccessory + "\",\"intColorTrim\":\"" + _userInfo.ColorTrim + "\",\"intMP\":" + _userInfo.MP + ",\"intLevel\":\"" + _userInfo.Level + "\",\"intColorSkin\":\"" + _userInfo.ColorSkin + "\",\"intMPMax\":" + _userInfo.MaxMP + ",\"intAccessLevel\":\"" + _userInfo.Access + "\",\"intHP\":" + _userInfo.HP + ",\"intColorBase\":\"" + _userInfo.ColorBase + "\",\"strHairFilename\":\"" + _userHair.Filename + "\",\"intHPMax\":" + _userInfo.MaxHP + ",\"intColorHair\":\"" + _userInfo.ColorHair + "\",\"HairID\":\"" + _userInfo.HairID + "\",\"intColorEye\":\"" + _userInfo.ColorEye + "\",\"strHairName\":\"" + _userHair.Name + "\",\"strGender\":\"" + _userInfo.Gender + "\",\"strUsername\":\"" + _userInfo.Username + "\",\"strClassName\":\"" + _userInfo.className + "\",\"eqp\":{" + userItemManager.getEquippedItems(_userInfo.userID) + "}}}}}";

                Connection.sendMessage(returnPacket);
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message);
            }
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
                userItemManager.loadItems(Connection.Session.userInfo.userID);
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
