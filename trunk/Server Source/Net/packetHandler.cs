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
            try
            {
                this.Connection.sendMessage("<msg t='sys'><body action='joinOK' r='137534'><pid id='3'/><vars /><uLs r='137534'><u i='143799' m='0' s='0' p='9'><n><![CDATA[karthikan]]></n><vars></vars></u><u i='145096' m='0' s='0' p='7'><n><![CDATA[beatlegger]]></n><vars></vars></u><u i='144837' m='0' s='0' p='5'><n><![CDATA[deathknight1oo]]></n><vars></vars></u><u i='145191' m='0' s='0' p='10'><n><![CDATA[raiogoku]]></n><vars></vars></u><u i='143062' m='0' s='0' p='1'><n><![CDATA[katty72]]></n><vars></vars></u><u i='145197' m='0' s='0' p='4'><n><![CDATA[eric_dantas01]]></n><vars></vars></u><u i='144671' m='0' s='0' p='6'><n><![CDATA[maskiterller]]></n><vars></vars></u><u i='145204' m='0' s='0' p='2'><n><![CDATA[starscreamss]]></n><vars></vars></u><u i='145205' m='0' s='0' p='3'><n><![CDATA[kiraz]]></n><vars></vars></u></uLs></body></msg>");
                this.Connection.sendMessage("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"moveToArea\",\"areaName\":\"battleon-5\",\"intKillCount\":0,\"uoBranch\":[[\"uoName:karthikan\",\"strUsername:Karthikan\",\"strFrame:Enter\",\"strPad:Spawn\",\"intState:1\",\"intLevel:5\",\"intHP:808\",\"intMP:24\",\"intHPMax:808\",\"intMPMax:24\",\"tx:37\",\"ty:433\",\"afk:true\"],[\"uoName:beatlegger\",\"strUsername:Beatlegger\",\"strFrame:Enter\",\"strPad:Spawn\",\"intState:1\",\"intLevel:14\",\"intHP:1005\",\"intMP:34\",\"intHPMax:1005\",\"intMPMax:34\",\"tx:0\",\"ty:0\",\"afk:true\"],[\"uoName:deathknight1oo\",\"strUsername:deathknight1oo\",\"strFrame:Enter\",\"strPad:Spawn\",\"intState:1\",\"intLevel:1\",\"intHP:630\",\"intMP:20\",\"intHPMax:630\",\"intMPMax:20\",\"tx:939\",\"ty:415\",\"afk:false\"],[\"uoName:raiogoku\",\"strUsername:raiogoku\",\"strFrame:Enter\",\"strPad:Spawn\",\"intState:1\",\"intLevel:12\",\"intHP:975\",\"intMP:32\",\"intHPMax:975\",\"intMPMax:32\",\"tx:594\",\"ty:422\",\"afk:false\"],[\"uoName:katty72\",\"strUsername:katty72\",\"strFrame:Enter\",\"strPad:Spawn\",\"intState:1\",\"intLevel:7\",\"intHP:878\",\"intMP:26\",\"intHPMax:878\",\"intMPMax:26\",\"tx:737\",\"ty:414\",\"afk:false\"],[\"uoName:eric_dantas01\",\"strUsername:eric_dantas01\",\"strFrame:Enter\",\"strPad:Spawn\",\"intState:1\",\"intLevel:6\",\"intHP:849\",\"intMP:26\",\"intHPMax:849\",\"intMPMax:26\",\"tx:0\",\"ty:0\",\"afk:false\"],[\"uoName:maskiterller\",\"strUsername:maskiterller\",\"strFrame:Enter\",\"strPad:Spawn\",\"intState:1\",\"intLevel:1\",\"intHP:630\",\"intMP:20\",\"intHPMax:630\",\"intMPMax:20\",\"tx:548\",\"ty:438\",\"afk:false\"],[\"uoName:starscreamss\",\"strUsername:Starscreamss\",\"strFrame:Enter\",\"strPad:Spawn\",\"intState:1\",\"intLevel:2\",\"intHP:500\",\"intMP:22\",\"intHPMax:500\",\"intMPMax:22\",\"tx:0\",\"ty:0\",\"afk:false\"],[\"uoName:kiraz\",\"strUsername:Kiraz\",\"strFrame:Enter\",\"strPad:Spawn\",\"intState:1\",\"intLevel:20\",\"intHP:500\",\"intMP:40\",\"intHPMax:500\",\"intMPMax:40\",\"tx:0\",\"ty:0\",\"afk:false\"]],\"strMapFileName\":\"Battleon/town-Battleon-12Feb10.swf\",\"intType\":\"2\",\"monBranch\":[],\"wB\":[],\"sExtra\":\"a1=test1,a2=test2\",\"areaId\":137534,\"strMapName\":\"battleon\"}}}");
                this.Connection.sendMessage("%xt%server%-1%You joined \"battleon-5\"!%");
                this.Connection.sendMessage("<msg t='sys'><body action='uER' r='137534'><u i ='145150' m='0' s='0' p='8'><n><![CDATA[rafu4]]></n><vars></vars></u></body></msg>");
                this.Connection.sendMessage("%xt%uotls%-1%rafu4%afk:false,intHP:722,strPad:Spawn,intMPMax:30,uoName:rafu4,tx:0,ty:0,intState:1,intLevel:10,strUsername:rafu4,intHPMax:946,intMP:0,strFrame:Enter%");
            }
            catch { }
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
