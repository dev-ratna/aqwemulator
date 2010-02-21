using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

using AQWE.Core;
using AQWE.Sessions;
using AQWE.Net;

namespace AQWE.Game.Managers
{
    /// <summary>
    /// Represents a generic character connection to the server.
    /// </summary>
    public class userManager
    {
        #region Declares
        /// <summary>
        /// The connection ID of this connection.
        /// </summary>
        internal int connectionID;
        /// <summary>
        /// The userSession object of this connection.
        /// </summary>
        internal userSession Session;
        /// <summary>
        /// The System.Net.Sockets.Socket object of this connection.
        /// </summary>
        private Socket Socket;
        /// <summary>
        /// A byte of array with the bytes of the message that is currently being recieved from the client via an asynchronous BeginRecieve operation.
        /// </summary>
        private byte[] dataBuffer;
        /// <summary>
        /// The last message that was recieved from the client.
        /// </summary>
        private string lastMessage;
        /// <summary>
        /// The packetHandler for this connection.
        /// </summary>
        private Packets packetHandler;

        private Queue sendQueue = new Queue();
        private bool sending = false;
        private string lastSentMessage = "none";
        #endregion

        #region Properties
        /// <summary>
        /// Returns the IP address of this connection as a string.
        /// </summary>
        internal string IP
        {
            get
            {
                return this.Socket.RemoteEndPoint.ToString().Split(':')[0];
            }
        }

        internal int isModerator
        {
            get
            {
                if (this.Session.userInfo.Access >= 3)
                    return 1;
                else
                    return 0;
            }
        }

        internal string isAFK
        {
            get
            {
                return this.Session.userInfo.Afk.ToString().ToLower();
            }
        }
        #endregion

        #region Contructors
        /// <summary>
        /// Initializes a new connection instance, greets the client and starts listening for data.
        /// </summary>
        /// <param name="connectionID">The ID of this connection.</param>
        /// <param name="Socket">The socket object of this connection.</param>
        public userManager(int connectionID, Socket Socket)
        {
            this.connectionID = connectionID;
            this.Socket = Socket;
            this.dataBuffer = new byte[1024];
            this.packetHandler = new Packets(this);

            this.Socket.BeginReceive(this.dataBuffer, 0, this.dataBuffer.Length, SocketFlags.None, new AsyncCallback(this.dataArrival), null);
        }
        #endregion

        #region Methods
        private void dataArrival(IAsyncResult iAr)
        {
            try
            {
                int byteRecieved = this.Socket.EndReceive(iAr);
                if (byteRecieved == 0)
                {
                    Sockets.endConnection(this, Session.userInfo.roomID);
                    return;
                }

                string Data = Encoding.UTF8.GetString(this.dataBuffer, 0, byteRecieved);
                if (Data.Contains("<") || Data.Contains("%") || Data.Contains("{") || Data.Contains("^"))
                {
                    if (Data != this.lastMessage)
                        this.packetHandler.Handle(Data);
                }

                this.lastMessage = Data;
            }
            catch (SocketException sEx) 
            { 
                Logging.logError(sEx.Message);
                Sockets.endConnection(this, this.Session.userInfo.roomID);
            }
            catch { Sockets.endConnection(this); }
            finally 
            {
                try
                {
                    this.Socket.BeginReceive(this.dataBuffer, 0, this.dataBuffer.Length, SocketFlags.None, new AsyncCallback(this.dataArrival), null);
                }
                catch
                {
                    try
                    {
                        Sockets.endConnection(this, Session.userInfo.roomID);
                    }
                    catch { Sockets.endConnection(this); }
                }
            }
        }

        /// <summary>
        /// Sends a message in string format to the client via an asynchronous BeginSend action.
        /// </summary>
        /// <param name="Message">The string object of message.</param>
        internal void sendMessage(string Message)
        {
            try
            {
                if (Message == null)
                    return;

                if (this.Socket.Connected == true)
                {
                        sendQueue.Enqueue(Message);
                        if (sendQueue.Count == 1 && sending == false)
                        {
                            Message = sendQueue.Dequeue().ToString();
                            byte[] Data = Encoding.UTF8.GetBytes(Message + Convert.ToChar(0x0));
                            this.Socket.BeginSend(Data, 0, Data.Length, SocketFlags.None, new AsyncCallback(sentMessage), null);
                            Logging.logServerMessage(this.connectionID, Message);
                            lastSentMessage = Message;
                            sending = true;
                            Data = null;
                            Message = null;
                        }
                }
            }
            catch (Exception ex)
            { Sockets.endConnection(this, Session.userInfo.roomID); Logging.logError(ex.Message); }
        }

        /// <summary>
        /// Ends an asynchronous BeginSend operation.
        /// </summary>
        /// <param name="iAr">The IAsyncResult object wich contains the data of the asynchronous operation.</param>
        private void sentMessage(IAsyncResult iAr)
        {
            if (sendQueue.Count > 0 && sending == false)
            {
                string Message = sendQueue.Dequeue().ToString();
                byte[] Data = Encoding.UTF8.GetBytes(Message + Convert.ToChar(0x0));
                this.Socket.BeginSend(Data, 0, Data.Length, SocketFlags.None, new AsyncCallback(sentMessage), null);
                Logging.logServerMessage(this.connectionID, Message);
                sending = true;
                Data = null;
            }
            else
            {
                try 
                {
                    this.Socket.EndSend(iAr);
                    sending = false;

                    if (sendQueue.Count > 0 && sending == false)
                    {
                        string Message = sendQueue.Dequeue().ToString();
                        byte[] Data = Encoding.UTF8.GetBytes(Message + Convert.ToChar(0x0));
                        this.Socket.BeginSend(Data, 0, Data.Length, SocketFlags.None, new AsyncCallback(sentMessage), null);
                        Logging.logServerMessage(this.connectionID, Message);
                        sending = true;
                        Data = null;
                    }
                }
                catch (Exception ex)
                { Sockets.endConnection(this, Session.userInfo.roomID); Logging.logError(ex.Message); }
            }
        }

        /// <summary>
        /// Closes this connection with a certain shutdown method.
        /// </summary>
        /// <bugged>
        /// The application seems to close as soon as the socket get's closed... need to find out how to fix this.
        /// </bugged>
        /// <param name="How">The SocketShutdown enumerator that specifies how the connection should be closed.</param>
        internal void Close(SocketShutdown How)
        {
            try
            {
                this.Socket.Shutdown(How);
                this.Socket.Close();
            }
            catch { Logging.logError("Error while shutting down socket"); }
        }
        #endregion
    }
}
