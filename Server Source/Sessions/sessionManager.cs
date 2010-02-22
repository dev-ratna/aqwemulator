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
            return Database.checkExists("users", "name", Username);
        }

        public static userSession getInstance(int userID)
        {
            if (_Sessions.ContainsKey(userID))
                return (userSession)_Sessions[userID];
            else
                throw new Exception("User id: " + userID + " doesn't exist");
        }

        public static void attemptLogin(string Username, string Password, Packets pH)
        {
            string[] Data = Database.runReadRowStrings("SELECT id,name,access,level,hair_id,password,salt FROM users WHERE name = '" + Username + "'");
            if (Data.Length > 0)
            {
                try
                {
                    User userInfo = new User();
                    userInfo.userID = int.Parse(Data[0]);
                    userInfo.Username = Data[1];
                    userInfo.Access = int.Parse(Data[2]);
                    userInfo.Level = int.Parse(Data[3]);
                    userInfo.HairID = int.Parse(Data[4]);
                    userInfo.connectionID = pH.Connection.connectionID;

                    userSession Session = new userSession(pH.Connection, userInfo);
                    pH.Connection.Session = Session;

                    try { _Sessions.Add(userInfo.userID, Session); }
                    catch 
                    {
                        _Sessions.Remove(userInfo.userID);
                        _Sessions.Add(userInfo.userID, Session);
                    }

                    pH.Connection.sendMessage("%xt%loginResponse%-1%true%" + userInfo.userID + "%" + userInfo.Username + "%" + Settings.server_motd + "%1262809466137%sNews=" + Settings.client_news + ",sMap=" + Settings.client_map + ",sBook=" + Settings.client_book + "%");
                }
                catch
                {
                    Logging.logWarning("Disconnected user because another user with same id is already logged in.");
                    Sockets.endConnection(pH.Connection, pH.Connection.Session.userInfo.roomID);
                }
            }
            else
            {
                Logging.logWarning("Disconnected user because of no data.");
                Sockets.endConnection(pH.Connection, pH.Connection.Session.userInfo.roomID);
            }
        }
        #endregion
    }
}
