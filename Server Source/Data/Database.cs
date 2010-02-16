using System;
using System.Data;
using System.Data.Odbc;
using System.Collections;

using AQWE.Core;

namespace AQWE.Data
{
    /// <summary>
    /// Provides high speed data access to the MySQL Database. Yep we all know <Azzimi> is hot<3
    /// </summary>
    public static class Database
    {
        private static OdbcConnection dbConnection;

        #region Database connection management
        /// <summary>
        /// Opens connection to the MySQL Database with the supplied parameters, and returns a 'true' boolean when connection has succeeded. Requires MySQL ODBC 5.1 Driver to be installed.
        /// </summary>
        /// <param name="_host">The hostname/IP where the MySQL Server is located.</param>
        /// <param name="_user">The username for authentication with the database.</param>
        /// <param name="_pass">The password for authentication with the database.</param>
        /// <param name="_db">The name of the database.</param>
        /// <param name="_port">The port the database server is running on.</param>
        public static bool openConnection(string _host, string _user, string _pass, string _db, int _port)
        {
            try
            {
                Logging.logHolyInfo("Connecting to database (" + _db + ") at " + _host + ":" + _port + " for user '" + _user + "'...");
                dbConnection = new OdbcConnection("Driver={MySQL ODBC 5.1 Driver};Server=" + _host + ";Port=" + _port + ";Database=" + _db + ";User=" + _user + ";Password=" + _pass + ";Option=3;");
                dbConnection.Open();
                Logging.logHolyInfo("Connection to database established.");
                return true;
            }
            catch (Exception ex)
            {
                Logging.logError(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Closes connection with the MySQL Database. Any errors are ignored.
        /// </summary>
        public static void closeConnection()
        {
            Logging.logHolyInfo("Closing database connection...");
            try
            {
                dbConnection.Close();
                Logging.logHolyInfo("Database connection closed.");
            }
            catch { Logging.logHolyInfo("No open database connection."); }
        }
        #endregion

        #region Database data manipulation
        #region runQuery
        /// <summary>
        /// Executes a raw SQL statement on the database.
        /// </summary>
        /// <param name="Query">The SQL statement to be executed. Default SQL Syntax.</param>
        public static void runQuery(string Query)
        {
            try { new OdbcCommand(Query, dbConnection).ExecuteScalar(); }
            catch (Exception ex) { Logging.logError(ex.Message + ", query = " + Query); }
        }

        /// <summary>
        /// Executes a SQL statement with given parameters at the database.
        /// </summary>
        /// <param name="paramIDs">The parameters, eg, name, password etc.</param>
        /// <param name="paramValues">The values, eg, 'Woody', 'pass'.</param>
        /// <param name="Query">The query with the parameters included, eg, UPDATE users SET password = @password WHERE username = @username LIMIT 1.</param>
        public static void runQuery(string[] paramIDs, object[] paramValues, string Query)
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand(Query, dbConnection);

                for (int i = 0; i < paramIDs.Length; i++)
                    cmd.Parameters.AddWithValue("@" + paramIDs[i], paramValues[i]);
                cmd.ExecuteNonQuery();
             
            }
            catch (Exception ex) { Logging.logError(ex.Message + ", query = " + Query); }
        }
        #endregion
        #endregion

        #region Database data retrievel
        #region runRead
        /// <summary>
        /// Performs a raw SQL query and returns the first selected field as string. Other fields are ignored.
        /// </summary>
        /// <param name="Query">The SQL query that selects a field.</param>
        public static string runRead(string Query)
        {
            try { return new OdbcCommand(Query + " LIMIT 1", dbConnection).ExecuteScalar().ToString(); }
            catch (Exception ex) 
            { 
                Logging.logError(ex.Message + ", query = " + Query);
                return "";
            }
        }

        /// <summary>
        /// Performs a raw SQL query and returns the first selected field as string. Other fields are ignored.
        /// </summary>
        /// <param name="paramIDs">The parameters, eg, name, password etc.</param>
        /// <param name="paramValues">The values, eg, 'Woody', 'pass'.</param>
        /// <param name="Query">The query with the parameters included, eg, SELECT email FROM users WHERE username = @name AND password = @password.</param>
        public static string runReadString(string[] paramIDs, object[] paramValues, string Query)
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand(Query + " LIMIT 1", dbConnection);
                for (int i = 0; i < paramIDs.Length; i++)
                    cmd.Parameters.AddWithValue("@" + paramIDs[i], paramValues[i]);
                cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex) { Logging.logError(ex.Message + ", query = " + Query); }
            
            return "";
        }

        /// <summary>
        /// Performs a raw SQL query and returns the first selected field as integer. Other fields are ignored.
        /// </summary>
        /// <param name="Query">The SQL query that selects a field.</param>
        public static int runReadInteger(string Query)
        {
            try { return Convert.ToInt32(new OdbcCommand(Query + " LIMIT 1", dbConnection).ExecuteScalar()); }
            catch (Exception ex)
            {
                Logging.logError(ex.Message + ", query = " + Query);
                return 0;
            }
        }

        /// <summary>
        /// Performs a SQL query with given parameters at the database and returns the first selected field as a integer. All other fields are ignored.
        /// </summary>
        /// <param name="paramIDs">The parameters, eg, name, password etc.</param>
        /// <param name="paramValues">The values, eg, 'Woody', 'pass'.</param>
        /// <param name="Query">The query with the parameters included, eg, SELECT gold FROM users WHERE username = @name AND password = @password.</param>
        public static int runReadInteger(string[] paramIDs, string[] paramValues, string Query)
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand(Query + " LIMIT 1", dbConnection);
                for (int i = 0; i < paramIDs.Length; i++)
                    cmd.Parameters.AddWithValue("@" + paramIDs[i], paramValues[i]);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message + ", query = " + Query);
                return 0;
            }
        }
        #endregion

        #region runReadColumn
        /// <summary>
        /// Performs a SQL query and returns all vertical matching fields as a string array. Only the first supplied column is looked for.
        /// </summary>
        /// <param name="Query">The SQL query that selects a column.</param>
        /// <param name="maxResults">Adds as LIMIT to the query. Using this, the array will never return more than xx fields in of the column. When maxResults is supplied as 0, then there is no max limit.</param>
        public static string[] runReadColumnStrings(string Query, int maxResults)
        {
            if (maxResults > 0)
                Query += " LIMIT " + maxResults;

            try
            {
                ArrayList columnBuilder = new ArrayList();
                OdbcDataReader columnReader = new OdbcCommand(Query, dbConnection).ExecuteReader();

                while (columnReader.Read())
                {
                    try { columnBuilder.Add(columnReader[0].ToString()); }
                    catch { columnBuilder.Add(""); }
                }
                columnReader.Close();

                return (string[])columnBuilder.ToArray(typeof(string));
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message + ", query = " + Query);
                return new string[0];
            }
        }

        /// <summary>
        /// Performs a SQL query and returns all vertical matching fields as a String array. Only first supplied column is looked for.
        /// </summary>
        /// <param name="paramIDs">The parameters, eg, name, class.</param>
        /// <param name="paramValues">The values, eg, 'Woody', 'warrior'.</param>
        /// <param name="Query">The query with the parameters included, eg, SELECT gold FROM users WHERE username = @name AND class = @class.</param>
        /// <param name="maxResults">Adds as LIMIT to the query. Using this the array will never return more than xx fields in of the column. When maxResults is supplied as 0, then there is no max limit.</param>
        public static string[] runReadColumnStrings(string[] paramIDs, string[] paramValues, string Query, int maxResults)
        {
            if (maxResults > 0)
                Query += " LIMIT " + maxResults;

            try
            {
                ArrayList columnBuilder = new ArrayList();
                OdbcCommand cmd = new OdbcCommand(Query, dbConnection);
                for (int i = 0; i < paramIDs.Length; i++)
                    cmd.Parameters.AddWithValue("@" + paramIDs[i], paramValues[i]);

                OdbcDataReader columnReader = cmd.ExecuteReader();
                while (columnReader.Read())
                {
                    try { columnBuilder.Add(columnReader[0].ToString()); }
                    catch { columnBuilder.Add(""); }
                }
                columnReader.Close();

                return (string[])columnBuilder.ToArray(typeof(string));
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message + ", query = " + Query);
                return new string[0];
            }
        }
        /// <summary> 
        /// Performs a SQL query and returns all vertical matching fields as an integer array. Only the first supplied columname is looked for. 
        /// </summary> 
        /// <param name="Query">The SQL query that selects a column.</param> 
        /// <param name="maxResults">Adds as LIMIT to the query. Using this, the array will never return more than xx fields in of the column. When maxResults is supplied as 0, then there is no max limit.</param> 
        public static int[] runReadColumnIntegers(string Query, int maxResults)
        {
            if (maxResults > 0)
                Query += " LIMIT " + maxResults;

            try
            {
                ArrayList columnBuilder = new ArrayList();
                OdbcDataReader columnReader = new OdbcCommand(Query, dbConnection).ExecuteReader();

                while (columnReader.Read())
                {
                    try { columnBuilder.Add((int)columnReader[0]); }
                    catch { columnBuilder.Add(0); }
                }
                columnReader.Close();

                return (int[])columnBuilder.ToArray(typeof(int));
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message + ", query = " + Query);
                return new int[0];
            }
        }

        /// <summary> 
        /// Performs a SQL query and returns all vertical matching fields as an integer array array. Only the first supplied columname is looked for. 
        /// </summary> 
        /// <param name="paramIDs">The parameters, eg, name, class etc.</param>
        /// <param name="paramValues">The values, eg, 'Woody', 'warrior'.</param>
        /// <param name="Query">The query with the parameters included, eg, SELECT gold FROM users WHERE username = @name AND class = @class.</param>
        /// <param name="maxResults">Adds as LIMIT to the query. Using this, the array will never return more than xx fields in of the column. When maxResults is supplied as 0, then there is no max limit.</param> 
        public static int[] runReadColumnIntegers(string[] paramIDs, string[] paramValues, string Query, int maxResults)
        {
            if (maxResults > 0)
                Query += " LIMIT " + maxResults;

            try
            {
                ArrayList columnBuilder = new ArrayList();
                OdbcCommand cmd = new OdbcCommand(Query, dbConnection);
                for (int i = 0; i < paramIDs.Length; i++)
                    cmd.Parameters.AddWithValue("@" + paramIDs[i], paramValues[i]);

                OdbcDataReader columnReader = cmd.ExecuteReader();
                while (columnReader.Read())
                {
                    try { columnBuilder.Add((int)columnReader[0]); }
                    catch { columnBuilder.Add(""); }
                }
                columnReader.Close();

                return (int[])columnBuilder.ToArray(typeof(int));
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message + ", query = " + Query);
                return new int[0];
            }
        }
        #endregion

        #region runReadRow
        /// <summary>
        /// Performs a SQL query and returns the selected in the first found row as a String array. Usable for only one row.
        /// </summary>
        /// <param name="Query">The SQL query that selectes a row and the fields to get. LIMIT 1 is added.</param>
        public static string[] runReadRowStrings(string Query)
        {
            try
            {
                ArrayList rowBuilder = new ArrayList();
                OdbcDataReader rowReader = new OdbcCommand(Query + " LIMIT 1", dbConnection).ExecuteReader();

                while (rowReader.Read())
                {
                    for (int i = 0; i < rowReader.FieldCount; i++)
                    {
                        try { rowBuilder.Add(rowReader[i].ToString()); }
                        catch { rowBuilder.Add(""); }
                    }
                }
                rowReader.Close();
                return (string[])rowBuilder.ToArray(typeof(string));
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message + ", query = " + Query);
                return new string[0];
            }
        }
        /// <summary> 
        /// Performs a SQL query and returns the selected in the first found row as a String array. Useable for only one row. 
        /// </summary> 
        /// <param name="Query">The SQL query that selects a row and the fields to get. LIMIT 1 is added.</param> 
        public static string[] runReadRowStrings(string[] paramIDs, object[] paramValues, string Query)
        {
            try
            {
                ArrayList rowBuilder = new ArrayList();
                OdbcCommand cmd = new OdbcCommand(Query + " LIMIT 1", dbConnection);
                for (int i = 0; i < paramIDs.Length; i++)
                    cmd.Parameters.AddWithValue("@" + paramIDs[i], paramValues[i]);
                OdbcDataReader rowReader = cmd.ExecuteReader();

                while (rowReader.Read())
                {
                    for (int i = 0; i < rowReader.FieldCount; i++)
                    {
                        try { rowBuilder.Add(rowReader[i].ToString()); }
                        catch { rowBuilder.Add(""); }
                    }
                }
                rowReader.Close();
                return (string[])rowBuilder.ToArray(typeof(string));
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message + ", query = " + Query);
                return new string[0];
            }
        }

        /// <summary>
        /// Performs a SQL query and returns the selected in the first found row as a Integer array. Usable for only one row.
        /// </summary>
        /// <param name="Query">The SQL query that selectes a row and the fields to get. LIMIT 1 is added.</param>
        public static int[] runReadRowIntegers(string Query)
        {
            try
            {
                ArrayList rowBuilder = new ArrayList();
                OdbcDataReader rowReader = new OdbcCommand(Query + " LIMIT 1", dbConnection).ExecuteReader();

                while (rowReader.Read())
                {
                    for (int i = 0; i < rowReader.FieldCount; i++)
                    {
                        try { rowBuilder.Add(rowReader.GetInt32(i)); }
                        catch { rowBuilder.Add(0); }
                    }
                }
                rowReader.Close();
                return (int[])rowBuilder.ToArray(typeof(int));
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message + ", query = " + Query);
                return new int[0];
            }
        }
        /// <summary> 
        /// Performs a SQL query and returns the selected in the first found row as a Integer array. Useable for only one row. 
        /// </summary> 
        /// <param name="Query">The SQL query that selects a row and the fields to get. LIMIT 1 is added.</param> 
        public static int[] runReadRowIntegers(string[] paramIDs, string[] paramValues, string Query)
        {
            try
            {
                ArrayList rowBuilder = new ArrayList();
                OdbcCommand cmd = new OdbcCommand(Query + " LIMIT 1", dbConnection);
                for (int i = 0; i < paramIDs.Length; i++)
                    cmd.Parameters.AddWithValue("@" + paramIDs[i], paramValues[i]);
                OdbcDataReader rowReader = cmd.ExecuteReader();

                while (rowReader.Read())
                {
                    for (int i = 0; i < rowReader.FieldCount; i++)
                    {
                        try { rowBuilder.Add(rowReader.GetInt32(i)); }
                        catch { rowBuilder.Add(0); }
                    }
                }
                rowReader.Close();
                return (int[])rowBuilder.ToArray(typeof(int));
            }
            catch (Exception ex)
            {
                Logging.logError(ex.Message + ", query = " + Query);
                return new int[0];
            }
        }
        #endregion
        #endregion

        #region Data availability checks
        /// <summary>
        /// Tries to find fields matching with the query. When there is atleast one result it returns true and stops.
        /// </summary>
        /// <param name="Query">The SQL query that contains the seeked fields and conditions. LIMIT 1 is added.</param>
        public static bool checkExists(string Query)
        {
            try { return new OdbcCommand(Query + " LIMIT 1", dbConnection).ExecuteReader().HasRows; }
            catch (Exception ex)
            {
                Logging.logError(ex.Message + ", query = " + Query);
                return false;
            }
        }

        public static bool checkExists(string Table, string Field, string fieldValue)
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand("SELECT " + Field + " FROM " + Table + " WHERE " + Field + " = @" + fieldValue + " LIMIT 1", dbConnection);
                cmd.Parameters.AddWithValue("@" + Field, fieldValue);
                return cmd.ExecuteReader().HasRows;
            }
            catch { return false; }
        }
        #endregion

        #region Misc
        /// <summary>
        /// Returns a stripslashed copy of the input string.
        /// </summary>
        /// <param name="Query">The string to add stripslashes to.</param>
        public static string Stripslash(string Query)
        {
            try { return Query.Replace(@"\", "\\").Replace("'", @"\'"); }
            catch { return ""; }
        }
        #endregion
    }
}
