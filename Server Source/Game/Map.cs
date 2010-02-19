using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Game.Managers;

namespace AQWE.Game
{
    public class Map
    {
        internal int ID;
        internal string Name;
        internal string Filename;

        public Map(int mapID)
        {
            this.ID = mapID;
            mapManager.Add(this.ID, this);
        }
    }
}
