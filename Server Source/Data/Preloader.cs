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

            Logging.logHolyInfo("");
            loadMaps();
            Logging.logHolyInfo("");
            loadHairs();
            Logging.logHolyInfo("");
            loadItems();
            Logging.logHolyInfo("");
            loadActions();
            Logging.logHolyInfo("");

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

        public static void loadItems()
        {
            Logging.logHolyInfo("Preloading items...");

            int[] itemIDs = Database.runReadColumnIntegers("SELECT id FROM items WHERE 1=1", 0);

            try
            {
                foreach (int tempID in itemIDs)
                {
                    Item _item = new Item(tempID);
                    _item.Name = Database.runRead("SELECT name FROM items WHERE id = " + tempID);
                    _item.Filename = Database.runRead("SELECT file_name FROM items WHERE id = " + tempID);
                    _item.Linkage = Database.runRead("SELECT linkage FROM items WHERE id = " + tempID);
                    _item.Type = Database.runRead("SELECT type FROM items WHERE id = " + tempID);

                    Logging.logHolyInfo("Preloaded '" + _item.Name + "'...");
                }
                Logging.logHolyInfo("Preloaded " + itemIDs.Length + " items.");
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message);
            }
        }

        public static void loadActions()
        {
            Logging.logHolyInfo("Preloading actions...");

            int[] actionIDs = Database.runReadColumnIntegers("SELECT id FROM actions WHERE 1=1", 0);

            try
            {
                foreach (int tempID in actionIDs)
                {
                    Game.Action _action = new Game.Action(tempID);
                    _action.Name        = Database.runRead("SELECT name FROM actions WHERE id = " + tempID);
                    _action.Icon        = Database.runRead("SELECT icon FROM actions WHERE id = " + tempID);
                    _action.Ref         = Database.runRead("SELECT ref FROM actions WHERE id = " + tempID);
                    _action.Description = Database.runRead("SELECT description FROM actions WHERE id = " + tempID);
                    _action.Animation   = Database.runRead("SELECT animation FROM actions WHERE id = " + tempID);
                    _action.Range       = int.Parse(Database.runRead("SELECT ragne FROM actions WHERE id = " + tempID));
                    _action.Fx          = Database.runRead("SELECT fx FROM actions WHERE id = " + tempID);
                    _action.Damage      = double.Parse(Database.runRead("SELECT damage FROM actions WHERE id = " + tempID));
                    _action.Mana        = int.Parse(Database.runRead("SELECT mana FROM actions WHERE id = " + tempID));
                    _action.Dsrc        = Database.runRead("SELECT dsrc FROM actions WHERE id = " + tempID);
                    _action.Auto        = bool.Parse(Database.runRead("SELECT auto FROM actions WHERE id = " + tempID));
                    _action.Tgt         = Database.runRead("SELECT tgt FROM actions WHERE id = " + tempID);
                    _action.Strl        = Database.runRead("SELECT strl FROM actions WHERE id = " + tempID);
                    _action.Cd          = int.Parse(Database.runRead("SELECT cd FROM actions WHERE id = " + tempID));
                    _action.Active      = bool.Parse(Database.runRead("SELECT active FROM actions WHERE id = " + tempID));

                    Logging.logHolyInfo("Preloaded '" + _action.Name + "'...");
                }

                Logging.logHolyInfo("Preloaded " + actionIDs.Length + " actions.");
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message);
            }
        }
    }
}
