using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Game.Managers;

namespace AQWE.Game
{
    public class Item
    {
        internal int ID;
        internal string Name;
        internal string Filename;
        internal string Linkage;
        internal string Type;

        public Item(int ItemID)
        {
            this.ID = ItemID;
            ItemManager.Add(this.ID, this);
        }
    }
}
