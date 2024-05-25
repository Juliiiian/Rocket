using System;
using Newtonsoft.Json;
using Oxide.Ext.Rocket.Logging;
using Oxide.Ext.Rocket.WebSockets.Handlers;
using Oxide.Core;
using WebSocketSharp;

namespace Oxide.Ext.Rocket.WebSockets
{

    /// <summary>
    /// Represents a listens for socket events
    /// </summary>
    public class SocketListener
    {
        /// <summary>
        /// The current session ID for the connected bot
        /// </summary>
        private string _sessionId;
        
        /// <summary>
        /// The current sequence number for the websocket
        /// </summary>
        private int _sequence;

        /// <summary>
        /// If the bot has successfully connected to the websocket at least once
        /// </summary>
        public bool SocketHasConnected { get; internal set; }

        private readonly Socket _webSocket;
        private readonly ILogger _logger;
        //private readonly SocketCommandHandler _commands;
        private HeartbeatHandler _heartbeat;

        /// <summary>
        /// Creates a new socket listener
        /// </summary>
        /// <param name="socket">Socket this listener is for</param>
        /// <param name="logger">Logger for the client</param>
        /// <param name="commands">Socket Command Handler</param>
        public SocketListener(Socket socket, ILogger logger)
        {
            _webSocket = socket;
            _logger = logger;
            //_commands = commands;
            _heartbeat = new HeartbeatHandler(_webSocket, this, _logger);
        }

        /// <summary>
        /// Shutdown the SocketListener
        /// </summary>
        public void Shutdown()
        {
            _heartbeat?.DestroyHeartbeat();
            _heartbeat = null;
        }

        #region Socket Events
        /// <summary>
        /// Called when a socket is open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SocketOpened(object sender, EventArgs e)
        {
            _logger.Info("WebSocket opened!");
            _webSocket.SocketState = SocketState.Connected;
            Interface.Call("OnWebSocketOpened");
        }

        /// <summary>
        /// Called when a socket is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SocketClosed(object sender, CloseEventArgs e)
        {
            //If the socket close came from the extension then this will be true
            if (sender is WebSocket socket && !_webSocket.IsCurrentSocket(socket))
            {
                _logger.Debug($"{nameof(SocketListener)}.{nameof(SocketClosed)} Socket closed event for non matching socket. Code: {e.Code.ToString()}, reason: {e.Reason}");
                return;
            }
            
            if (e.Code == 1000 || e.Code == 4199)
            {
                _logger.Debug($"{nameof(SocketListener)}.{nameof(SocketClosed)} Discord WebSocket closed. Code: {e.Code.ToString()}, reason: {e.Reason}");
            }
            
            Interface.Call("OnWebSocketClosed", e.Reason, e.Code, e.WasClean);
            _webSocket.SocketState = SocketState.Disconnected;
            _webSocket.DisposeSocket();
            
            if (_webSocket.RequestedReconnect)
            {
                _webSocket.RequestedReconnect = false;
                _webSocket.Reconnect();
                return;
            }
        }

       

        /// <summary>
        /// Called when an error occurs on a socket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SocketErrored(object sender, ErrorEventArgs e)
        {
            if (sender is WebSocket socket && !_webSocket.IsCurrentSocket(socket))
            {
                return;
            }

            Interface.Call("OnWebSocketErrored", e.Exception, e.Message);
            _logger.Exception("An error has occured in the websocket. Attempting to reconnect to discord.", e.Exception);
            _webSocket.Disconnect(true, false);
        }

        /// <summary>
        /// Called when a socket receives a message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SocketMessage(object sender, MessageEventArgs e)
        {
            _logger.Debug(e.Data);
            
            EventPayload<ServerToSocketEvents> payload = JsonConvert.DeserializeObject<EventPayload<ServerToSocketEvents>>(e.Data);

            if (_logger.IsLogging(Logging.LogLevel.Verbose))
            {
                _logger.Verbose($"Received socket message, EventName: {payload.EventName.ToString()}\nContent:\n{e.Data}");
            }
            else
            {
                _logger.Debug($"Received socket message, EventName: {payload.EventName.ToString()}");
            }

            try
            {
                MainHandler.HandleSocketMessage(payload, _logger);
            }
            catch (Exception ex)
            {
                _logger.Exception($"{nameof(SocketListener)}.{nameof(SocketMessage)} Exception Occured. Please give error message below to Rocket Extension Authors:\n", ex);
            }
        }
        #endregion
    }
}
