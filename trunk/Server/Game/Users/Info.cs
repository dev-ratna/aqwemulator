using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AQWE.Game.Users
{
    /// <summary>
    /// Contains info for a logged in user.
    /// </summary>
    public class Info
    {
        #region Declares
        /// <summary>
        /// The ID of the connection/session of this user.
        /// </summary>
        internal int connectionID;
        /// <summary>
        /// The database ID of this user.
        /// </summary>
        internal int userID;
        /// <summary>
        /// The username of this user.
        /// </summary>
        internal string Username;
        /// <summary>
        /// The rank of this user.
        /// </summary>
        internal int Rank;
        #endregion
    }
}
