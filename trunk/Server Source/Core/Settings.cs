using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AQWE.Data;
using AQWE.Core;

namespace AQWE.Core
{
    /// <summary>
    /// Contains server, mysql and other important settings.
    /// </summary>
    class Settings
    {
        #region Declares
        #region MySQL Server Configuration
        /// <summary>
        /// MySQL Server Hostname.
        /// </summary>
        public static string mysql_host = "localhost";
        /// <summary>
        /// MySQL Server Username.
        /// </summary>
        public static string mysql_username = "root";
        /// <summary>
        /// MySQL Server Password.
        /// </summary>
        public static string mysql_password = "";
        /// <summary>
        /// MySQL Server Database name.
        /// </summary>
        public static string mysql_db = "aqw_db";
        /// <summary>
        /// MySQL Server Port.
        /// </summary>
        public static int mysql_port = 3306;
        #endregion

        #region Socket Server Configuration
        /// <summary>
        /// Port number that the server is going to listen to.
        /// </summary>
        public static int server_port = 5588;
        /// <summary>
        /// The max amount of online users.
        /// </summary>
        public static int server_max_connections = 200;
        /// <summary>
        /// The max length of the connection queue.
        /// </summary>
        public static int server_back_log = 10;
        #endregion

        #region Server Configuration
        public static string server_name = "localhost";
        public static string server_motd = "Private server concept and design by Syntax & Divien.";
        #endregion

        #region Client Configuration
        public static string client_news = "news/News-05Feb10.swf";
        public static string client_map = "news/Map-Jan1510.swf";
        public static string client_book = "news/Book-5Feb10.swf";
        /// <summary>
        /// The maximum limit of users allowed in a room.
        /// </summary>
        public static int room_user_limit = 10;
        /// <summary>
        /// The id of map that user loads when user joins first time.
        /// </summary>
        public static int map_first_join = 1;
        #endregion
        #endregion

        #region Methods
        /// <summary>
        /// Initializes various settings from system_config table and stores them in this class.
        /// </summary>
        public static void Init()
        {
            Logging.logHolyInfo("Initializing settings...");

            // INIT SOCKET SERVER SETTINGS
            server_port = getIntEntry("server.port");
            server_max_connections = getIntEntry("server.max.connections");
            server_back_log = getIntEntry("server.back.log");

            // INIT SERVER SETTINGS
            server_name = getStringEntry("server.name");
            server_motd = getStringEntry("server.motd");

            // INIT CLIENT SETTINGS
            client_news = getStringEntry("client.news");
            client_map = getStringEntry("client.map");
            client_book = getStringEntry("client.book");
            room_user_limit = getIntEntry("room.user.limit");
            map_first_join = getIntEntry("map.first.join");

            Logging.logHolyInfo("Settings initialized.");
        }

        /// <summary>
        /// Gets the value from a config entry in the system_config table, and returns it as a string.
        /// </summary>
        /// <param name="key">The key of the config entry.</param>
        public static string getStringEntry(string key)
        {
            return Database.runRead("SELECT cvalue FROM system_config WHERE ckey = '" + key + "'");
        }

        /// <summary>
        /// Gets the value froma config entry in the system_config table, and returns it as a integer.
        /// </summary>
        /// <param name="key">The key of the config entry.</param>
        public static int getIntEntry(string key)
        {
            return Database.runReadInteger("SELECT cvalue FROM system_config WHERE ckey = '" + key + "'");
        }
        #endregion
    }
}
