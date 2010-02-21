using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

using AQWE.Core;
using AQWE.Data;
using AQWE.Game.Rooms;
using AQWE.Game.Managers;

namespace AQWE.Net
{
    /// <summary>
    /// Provides management for connected game clients.
    /// </summary>
    class Sockets
    {
        #region Declares
        /// <summary>
        /// The sockets that listens for new connection requests.
        /// </summary>
        private static Socket Listener;
        /// <summary>
        /// The dictionary that contains the gameConnection objects for the connections.
        /// </summary>
        private static Dictionary<int, userManager> Connections;
        /// <summary>
        /// A number that keeps the amount of accepted connections. Incremented by one at each request.
        /// </summary>
        private static int connectionCounter = 0;
        #endregion

        #region Methods
        public static userManager getInstance(int connectionID)
        {
            if (Connections.ContainsKey(connectionID))
                return (userManager)Connections[connectionID];
            else
                throw new Exception("Connection id: " + connectionID + " doesn't exist.");
        }
        /// <summary>
        /// Attempts to start listening on a certain port, with a certain amount of max connections and a certain max length of connection queue. A boolean that indicates if the operation has succeeded is returned.
        /// </summary>
        /// <param name="Port">The number of the TCP port to start listening on.</param>
        /// <param name="MaxConnections">The max amount of simultaneous connections.</param>
        /// <param name="backLog">The max length of the connection queue.</param>
        public static bool Listen(int Port, int MaxConnections, int backLog)
        {
            try
            {
                Logging.logHolyInfo("Starting up socket listener on port " + Port + "...");

                Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Listener.Bind(new IPEndPoint(IPAddress.Any, Port));
                Listener.Listen(backLog);

                Connections = new Dictionary<int, userManager>();

                Listener.BeginAccept(new AsyncCallback(connectionRequest), Listener);
                Logging.logHolyInfo("Socket listener running on port " + Port + ".");
                Logging.logHolyInfo("Max simultaneous connections is " + MaxConnections + ".");
                Logging.logHolyInfo("Max length of connection request queue (backlog) is " + backLog + ".");
                return true;
            }
            catch
            {
                Logging.logWarning("Failed to set up connection listener on port " + Port + ". Port could be in use etc.");
                return false;
            }
        }

        /// <summary>
        /// Invoked asynchronously when a new client requests connection. If the connection is not blacklisted and the max simultaenous connections amount isn't reached yet, then the connection will be processed and normal packet procedure will start.
        /// </summary>
        /// <param name="iAr">The IAsyncResult object of the asynchronous BeginAccept operation.</param>
        private static void connectionRequest(IAsyncResult iAr)
        {
            try
            {
                Socket newClient = Listener.EndAccept(iAr);
                string newClientIP = newClient.RemoteEndPoint.ToString().Split(':')[0];

                if (Connections.Count > Settings.server_max_connections)
                {
                    newClient.Close();
                    Logging.logInfo("Refused connection request from " + newClientIP + ", because the max amount of simultaneous connections (" + Settings.server_max_connections + ") has been reached.");
                }
                else if (Database.checkExists("connectionblacklist", "ipaddress", newClientIP))
                {
                    newClient.Close();
                    Logging.logInfo("Refused connection request from " + newClientIP + ", because this IP address is on the connection blacklist.");
                }
                else
                    createConnection(newClient);
            }
            catch (Exception ex) { Logging.logError(ex.Message); }
            Listener.BeginAccept(new AsyncCallback(connectionRequest), Listener);
        }

        /// <summary>
        /// Creates a new connectionManager object for a new client connection, and starts up the packet routine.
        /// </summary>
        /// <param name="newClient">The Socket object of the new client connection.</param>
        private static void createConnection(Socket newClient)
        {
            try
            {
                connectionCounter++;
                userManager Connection = new userManager(connectionCounter, newClient);
                Connections.Add(Connection.connectionID, Connection);

                Logging.mainForm.updateOnlineUsers(Connections.Count);
                Logging.logInfo("Created connection (" + Connection.connectionID + ") for " + Connection.IP + ".");
            }
            catch { }
        }

        /// <summary>
        /// Ends a certain connection to the game server and cleans up used resources.
        /// </summary>
        /// <param name="Connection">The connectionManager object of the connection to end.</param>
        public static void endConnection(userManager Connection)
        {
            if (Connections.ContainsKey(Connection.connectionID))
            {
                //Sessions.sessionManager._Sessions.Remove(Connection.Session.userInfo.userID);
                Logging.logInfo("Ended connection " + Connection.connectionID + "[" + Connection.IP + "]");
                Connection.Close(SocketShutdown.Both);
                Connections.Remove(Connection.connectionID);

                Logging.mainForm.updateOnlineUsers(Connections.Count);
            }
        }

        /// <summary>
        /// Ends a certain connection to the game server and cleans up used resources.
        /// </summary>
        /// <param name="Connection">The connectionManager object of the connection to end.</param>
        public static void endConnection(userManager Connection, int _roomID)
        {
            if (Connections.ContainsKey(Connection.connectionID))
            {
                if (_roomID != -1)
                    roomManager.UserLeft(Connection, _roomID);

                //Sessions.sessionManager._Sessions.Remove(Connection.Session.userInfo.userID);
                Logging.logInfo("Ended connection " + Connection.connectionID + "[" + Connection.IP + "]");
                Connection.Close(SocketShutdown.Both);
                Connections.Remove(Connection.connectionID);

                Logging.mainForm.updateOnlineUsers(Connections.Count);
            }
        }

        public static void stopConnection()
        {
            Listener.Close();
        }
        #endregion
    }
}
