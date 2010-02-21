using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Game.Managers;

namespace AQWE.Game.Users
{
    public class Item
    {
        internal int ID;
        internal int ItemID;
        internal int UserID;
        internal bool Equipped;

        public Item(int _ID)
        {
            this.ID = _ID;
            userItemManager.Add(this.ID, this);
        }
    }
}
