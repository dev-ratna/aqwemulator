using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using AQWE.Core;
using AQWE.Sessions;

namespace AQWE.Net
{
    public class packetHandler
    {
        internal connectionManager Connection;

        public packetHandler(connectionManager Connection)
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

        public void handleJoin(string[] Message)
        {
            this.Connection.sendMessage("<msg t='sys'><body action='joinOK' r='1'><pid id='" + Connection.Session.userInfo.userID + "'/><vars /></body></msg>");
            this.Connection.sendMessage("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"moveToArea\",\"areaName\":\"battleon-1\",\"intKillCount\":0,\"uoBranch\":[{\"strFrame\":\"Enter\",\"intMP\":100,\"intLevel\":\"20\",\"strPad\":\"Spawn\",\"intMPMax\":100,\"intHP\":100,\"afk\":false,\"ty\":0,\"intHPMax\":100,\"tx\":0,\"intState\":1,\"strUsername\":\"" + Connection.Session.userInfo.Username + "\",\"uoName\":\"" + Connection.Session.userInfo.Username + "\"}],\"strMapFileName\":\"\\Battleon\\town-Battleon-4Feb10.swf\",\"intType\":\"2\",\"monBranch\":[],\"sExtra\":\"a1=test1,a2=test2\",\"areaId\":30664,\"strMapName\":\"battleon\"}}}");
            this.Connection.sendMessage("%xt%server%-1%You joined \"battleon-1\"!%");
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
