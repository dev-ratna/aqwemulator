using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Game.Managers;

namespace AQWE.Game
{
    public class Hair
    {
        internal int ID;
        internal string Name;
        internal string Filename;

        public Hair(int hairID)
        {
            this.ID = hairID;
            hairManager.Add(this.ID, this);
        }
    }
}
