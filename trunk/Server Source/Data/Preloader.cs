﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Core;
using AQWE.Data;
using AQWE.Game;

namespace AQWE.Data
{
    public static class Preloader
    {
        /// <summary>
        /// Preloads the game database.
        /// </summary>
        public static void Init()
        {
            Logging.logHolyInfo("Preloading game database...");
            loadMaps();
            Logging.logHolyInfo("Game database preloaded.");
        }

        /// <summary>
        /// Preloads all maps from the database.
        /// </summary>
        public static void loadMaps()
        {
            Logging.logHolyInfo("Preloading maps...");

            int[] mapIDs = Database.runReadColumnIntegers("SELECT id FROM maps WHERE 1=1", 0);

            try
            {
                foreach (int tempID in mapIDs) {
                    Map _map = new Map(tempID);

                    _map.Name       = Database.runRead("SELECT name FROM maps WHERE id = '" + tempID + "'");
                    _map.Filename   = Database.runRead("SELECT file_name FROM maps WHERE id = '" + tempID + "'");

                    Logging.logHolyInfo("Preloaded '" + _map.Name + "'...");
                }
                Logging.logHolyInfo("Preloaded " + mapIDs.Length + " maps.");
            }
            catch (Exception ex) { Logging.logError(ex.Message); }
        }
    }
}