using System;
using System.Collections.Generic;

using AQWE.Core;
using AQWE.Data;
using AQWE.Net;
using AQWE.Game.Users;
using AQWE.Security;

namespace AQWE.Sessions
{
    /// <summary>
    /// Provides management for logged in users.
    /// </summary>
    public static class sessionManager
    {
        #region Declares
        /// <summary>
        /// A keyed collection with the userSession objects of the logged in users.
        /// </summary>
        public static Dictionary<int, userSession> _Sessions = new Dictionary<int, userSession>();
        #endregion

        #region Properties
        /// <summary>
        /// The amount of sessions (aka, logged in users) in the session manager.
        /// </summary>
        public static int Count
        {
            get
            {
                return _Sessions.Count;
            }
        }
        #endregion

        #region Methods
        public static bool userExists(string Username)
        {
            return Database.checkExists("users", "username", Username);
        }

        public static void attemptLogin(string Username, string Password, packetHandler pH)
        {
            string[] Data = Database.runReadRowStrings("SELECT id,name,access,password,salt FROM users WHERE name = '" + Username + "'");
            if (Data.Length > 0)
            {
                Info userInfo = new Info();
                userInfo.userID = int.Parse(Data[0]);
                userInfo.Username = Data[1];
                userInfo.Rank = int.Parse(Data[2]);
                userInfo.connectionID = pH.Connection.connectionID;

                userSession Session = new userSession(pH.Connection, userInfo);
                pH.Connection.Session = Session;
                _Sessions.Add(userInfo.userID, Session);

                pH.Connection.sendMessage("%xt%loginResponse%-1%true%" + userInfo.userID + "%" + userInfo.Username + "%" + Settings.server_motd + "%1262809466137%sNews=" + Settings.server_news + ",sMap=" + Settings.server_map + ",sBook=" + Settings.server_book + "%");
            }
            else
            {
                Logging.logWarning("Disconnected user because of no data.");
                socketManager.endConnection(pH.Connection);
            }
        }
        #endregion
    }
}
