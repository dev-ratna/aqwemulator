using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Game;

namespace AQWE.Game.Managers
{
    public static class hairManager
    {
        public static Dictionary<int, Hair> Hairs = new Dictionary<int, Hair>();

        public static Hair getInstance(int hairID)
        {
            if (Hairs.ContainsKey(hairID))
                return (Hair)Hairs[hairID];
            else
                throw new Exception("Hair id: " + hairID + " doesn't exist");
        }

        public static void Add(int hairID, Hair hairClass)
        {
            if (!Hairs.ContainsKey(hairID))
                Hairs.Add(hairID, hairClass);
        }

        public static void Remove(int hairID)
        {
            if (Hairs.ContainsKey(hairID))
                Hairs.Remove(hairID);
        }
    }
}
