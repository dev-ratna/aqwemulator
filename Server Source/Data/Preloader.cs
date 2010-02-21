using System;
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
            loadHairs();
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

                    _map.Name       = Database.runRead("SELECT name FROM maps WHERE id = " + tempID);
                    _map.Filename   = Database.runRead("SELECT file_name FROM maps WHERE id = " + tempID);

                    Logging.logHolyInfo("Preloaded '" + _map.Name + "'...");
                }
                Logging.logHolyInfo("Preloaded " + mapIDs.Length + " maps.");
            }
            catch (Exception ex) { Logging.logError(ex.Message); }
        }

        /// <summary>
        /// Preloads all hairs from the database.
        /// </summary>
        public static void loadHairs()
        {
            Logging.logHolyInfo("Preloading hairs...");

            int[] hairIDs = Database.runReadColumnIntegers("SELECT id FROM hairs WHERE 1=1", 0);

            try
            {
                foreach (int tempID in hairIDs)
                {
                    Hair _hair = new Hair(tempID);
                    _hair.Name = Database.runRead("SELECT name FROM hairs WHERE id = " + tempID);
                    _hair.Filename = Database.runRead("SELECT file_name FROM hairs WHERE id = " + tempID);
                }
                Logging.logHolyInfo("Preloaded " + hairIDs.Length + " hairs.");
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message);
            }
        }
    }
}
