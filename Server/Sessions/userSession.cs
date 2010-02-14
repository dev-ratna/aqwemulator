using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Net;
using AQWE.Game.Users;

namespace AQWE.Sessions
{
    /// <summary>
    /// Represents a session of a character.
    /// </summary>
    public class userSession
    {
        #region Declares
        /// <summary>
        /// The userInfo object of this user.
        /// </summary>
        internal Info userInfo;
        /// <summary>
        /// The connectionManager object of this session.
        /// </summary>
        private connectionManager Connection;
        #endregion

        #region Contructors
        public userSession(connectionManager Connection, Info userInfo)
        {
            this.Connection = Connection;
            this.userInfo = userInfo;
        }
        #endregion
    }
}
