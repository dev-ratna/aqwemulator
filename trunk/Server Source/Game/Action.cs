using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Game.Managers;

namespace AQWE.Game
{
    public class Action
    {
        internal int ID;
        internal string Name;
        internal string Icon;
        internal string Ref;
        internal string Description;
        internal string Animation;
        internal int Range;
        internal string Fx;
        internal double Damage;
        internal int Mana;
        internal string Dsrc;
        internal bool Auto;
        internal string Tgt;
        internal string Typ;
        internal string Strl;
        internal int Cd;
        internal bool Active;

        public Action(int actionID)
        {
            this.ID = actionID;
            actionManager.Add(this.ID, this);
        }
    }
}
