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
                                        this.Connection.sendMessage("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"cvu\",\"o\":{\"PCstRatio\":3.73,\"PChpDelta\":1640,\"baseHit\":0,\"PChpBase1\":360,\"resistRating\":17,\"baseCritValue\":1.7,\"PChpGoal100\":4000,\"intLevelCap\":100,\"baseMiss\":0.06,\"baseParry\":0.03,\"GstBase\":6,\"modRating\":3,\"baseResistValue\":0.7,\"baseBlockValue\":0.7,\"intHPperEND\":10,\"baseHaste\":0,\"baseBlock\":0,\"PChpBase100\":2000,\"intAPtoDPS\":20,\"baseCrit\":0.05,\"PCstBase\":8,\"baseEventValue\":0.05,\"PChpGoal1\":400,\"GstRatio\":2.8,\"intLevelMax\":100,\"bigNumberBase\":8,\"baseDodge\":0.04,\"PCDPSMod\":0.85}}}}");
                                        this.Connection.sendMessage("<msg t='sys'><body action='joinOK' r='30664'><pid id='9'/><vars /><uLs r='30664'><u i='24448' m='0' s='0' p='7'><n><![CDATA[wikto]]></n><vars></vars></u><u i='24248' m='0' s='0' p='5'><n><![CDATA[jitsulover08]]></n><vars></vars></u><u i='24510' m='0' s='0' p='10'><n><![CDATA[theseth]]></n><vars></vars></u><u i='24525' m='0' s='0' p='4'><n><![CDATA[prince lunar]]></n><vars></vars></u><u i='24526' m='0' s='0' p='2'><n><![CDATA[leo300loko2]]></n><vars></vars></u><u i='24529' m='0' s='0' p='6'><n><![CDATA[gkendall]]></n><vars></vars></u><u i='24531' m='0' s='0' p='8'><n><![CDATA[bid19]]></n><vars></vars></u><u i='24534' m='0' s='0' p='3'><n><![CDATA[aura_kim]]></n><vars></vars></u><u i='24535' m='0' s='0' p='1'><n><![CDATA[thewhitenightdragon]]></n><vars></vars></u><u i='24536' m='0' s='0' p='9'><n><![CDATA[kiraz]]></n><vars></vars></u></uLs></body></msg>");
                                        this.Connection.sendMessage("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"moveToArea\",\"areaName\":\"battleon-2\",\"intKillCount\":0,\"uoBranch\":[{\"strFrame\":\"Enter\",\"intMP\":100,\"intLevel\":\"20\",\"strPad\":\"Spawn\",\"intMPMax\":100,\"intHP\":938,\"afk\":true,\"ty\":377,\"intHPMax\":938,\"tx\":165,\"intState\":1,\"strUsername\":\"wikto\",\"uoName\":\"wikto\"},{\"strFrame\":\"Enter\",\"intMP\":100,\"intLevel\":\"17\",\"strPad\":\"Spawn\",\"intMPMax\":100,\"intHP\":759,\"afk\":false,\"ty\":484,\"intHPMax\":759,\"tx\":697,\"intState\":1,\"strUsername\":\"Jitsulover08\",\"uoName\":\"jitsulover08\"},{\"strFrame\":\"Enter\",\"intMP\":100,\"intLevel\":\"15\",\"strPad\":\"Spawn\",\"intMPMax\":100,\"intHP\":826,\"afk\":false,\"ty\":0,\"intHPMax\":826,\"tx\":0,\"intState\":1,\"strUsername\":\"theseth\",\"uoName\":\"theseth\"},{\"strFrame\":\"Enter\",\"intMP\":100,\"intLevel\":\"20\",\"strPad\":\"Spawn\",\"intMPMax\":100,\"intHP\":968,\"afk\":false,\"ty\":0,\"intHPMax\":968,\"tx\":0,\"intState\":1,\"strUsername\":\"Prince Lunar\",\"uoName\":\"prince lunar\"},{\"strFrame\":\"Enter\",\"intMP\":100,\"intLevel\":\"20\",\"strPad\":\"Spawn\",\"intMPMax\":100,\"intHP\":1008,\"afk\":false,\"ty\":448,\"intHPMax\":1008,\"tx\":632,\"intState\":1,\"strUsername\":\"leo300loko2\",\"uoName\":\"leo300loko2\"},{\"strFrame\":\"Enter\",\"intMP\":100,\"intLevel\":\"20\",\"strPad\":\"Spawn\",\"intMPMax\":100,\"intHP\":848,\"afk\":false,\"ty\":334,\"intHPMax\":848,\"tx\":257,\"intState\":1,\"strUsername\":\"gkendall\",\"uoName\":\"gkendall\"},{\"strFrame\":\"Enter\",\"intMP\":100,\"intLevel\":\"10\",\"strPad\":\"Spawn\",\"intMPMax\":100,\"intHP\":624,\"afk\":false,\"ty\":0,\"intHPMax\":624,\"tx\":0,\"intState\":1,\"strUsername\":\"bid19\",\"uoName\":\"bid19\"},{\"strFrame\":\"Enter\",\"intMP\":100,\"intLevel\":\"20\",\"strPad\":\"Spawn\",\"intMPMax\":100,\"intHP\":1318,\"afk\":false,\"ty\":0,\"intHPMax\":1318,\"tx\":0,\"intState\":1,\"strUsername\":\"Aura_Kim\",\"uoName\":\"aura_kim\"},{\"strFrame\":\"Enter\",\"intMP\":100,\"intLevel\":\"12\",\"strPad\":\"Spawn\",\"intMPMax\":100,\"intHP\":687,\"afk\":false,\"ty\":447,\"intHPMax\":687,\"tx\":574,\"intState\":1,\"strUsername\":\"TheWhiteNightDragon\",\"uoName\":\"thewhitenightdragon\"},{\"strFrame\":\"Enter\",\"intMP\":100,\"intLevel\":\"20\",\"strPad\":\"Spawn\",\"intMPMax\":100,\"intHP\":100,\"afk\":false,\"ty\":0,\"intHPMax\":100,\"tx\":0,\"intState\":1,\"strUsername\":\"Kiraz\",\"uoName\":\"kiraz\"}],\"strMapFileName\":\"\\Battleon\\town-Battleon-4Feb10.swf\",\"intType\":\"2\",\"monBranch\":[],\"sExtra\":\"a1=test1,a2=test2\",\"areaId\":30664,\"strMapName\":\"battleon\"}}}");
                                        this.Connection.sendMessage("%xt%server%-1%You joined \"battleon-2\"!%");
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

        public void handleXML(string Message)
        {
            if (Message.Contains("<policy"))
            {
                this.Connection.sendMessage("<cross-domain-policy><allow-access-from domain='*' to-ports='" + Settings.server_port + "' /></cross-domain-policy>");
            }
            else if (Message.Contains("verChk"))
            {
                this.Connection.sendMessage("<cross-domain-policy><allow-access-from domain='*' to-ports='" + Settings.server_port + "' /></cross-domain-policy>");
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
