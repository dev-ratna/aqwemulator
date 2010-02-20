using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AQWE.Game.Users
{
    /// <summary>
    /// Contains info for a logged in user.
    /// </summary>
    public class User
    {
        #region Declares
        /// <summary>
        /// The ID of the connection/session of this user.
        /// </summary>
        internal int connectionID = -1;
        /// <summary>
        /// The database ID of this user.
        /// </summary>
        internal int userID = -1;
        /// <summary>
        /// The username of this user.
        /// </summary>
        internal string Username = "none";
        /// <summary>
        /// The access of this user.
        /// </summary>
        internal int Access = 0;
        /// <summary>
        /// The id of the room that the user is in.
        /// </summary>
        internal int roomID = -1;
        /// <summary>
        /// User's id in a room.
        /// </summary>
        internal int playerID = -1;
        /// <summary>
        /// User's state.
        /// </summary>
        internal byte State = 1;
        /// <summary>
        /// User's level.
        /// </summary>
        internal int Level = 0;
        /// <summary>
        /// Health Points of user.
        /// </summary>
        internal int HP = 1101;
        /// <summary>
        /// Mana Points of user.
        /// </summary>
        internal int MP = 40;
        /// <summary>
        /// The maximum HP that the user can have.
        /// </summary>
        internal int MaxHP = 1101;
        /// <summary>
        /// The maximum MP that the user can have.
        /// </summary>
        internal int MaxMP = 40;
        /// <summary>
        /// User's X position.
        /// </summary>
        internal int X = 0;
        /// <summary>
        /// User's Y position.
        /// </summary>
        internal int Y = 0;
        /// <summary>
        /// The AFK state of the user.
        /// </summary>
        internal bool Afk = false;
        /// <summary>
        /// User's frame.
        /// </summary>
        internal string Frame = "Enter";
        /// <summary>
        /// User's pad.
        /// </summary>
        internal string Pad = "Spawn";
        internal int ColorAccessory = 10027008;
        internal int BagSlots = 40;
        internal int UpgradeDays = 400;
        internal int ColorBase = 8556972;
        #endregion
    }
}
