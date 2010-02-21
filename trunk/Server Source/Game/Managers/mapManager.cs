using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Game;

namespace AQWE.Game.Managers
{
    public static class mapManager
    {
        public static Dictionary<int, Map> Maps = new Dictionary<int, Map>();

        public static Map getInstance(int mapID)
        {
            if (Maps.ContainsKey(mapID))
                return (Map)Maps[mapID];
            else
                throw new Exception("The map id: " + mapID + ". Doesn't exist.");
        }

        /// <summary>
        /// Adds a new map to the map manager.
        /// </summary>
        /// <param name="mapID">The id of the map to add.</param>
        /// <param name="mapClass">The class of the map to add.</param>
        public static void Add(int mapID, Map mapClass)
        {
            if (!Maps.ContainsKey(mapID))
                Maps.Add(mapID, mapClass);
        }

        /// <summary>
        /// Removes a map from the map manager.
        /// </summary>
        /// <param name="mapID">The id of the map to remove.</param>
        public static void Remove(int mapID)
        {
            if (Maps.ContainsKey(mapID))
                Maps.Remove(mapID);
        }
    }
}
