using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oxide.Ext.Rocket.WebSockets
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class EventPayload<T>
    {
        /// <summary>
        /// Op Code for the payload
        /// </summary>
        [JsonProperty("ev")]
        public T EventName { get; set; }

        /// <summary>
        /// Event data
        /// </summary>
        [JsonProperty("d")]
        public object Data { get; set; }
    }

    public enum SocketToServerEvents
    {
        /// <summary>
        /// Ping-Pong
        /// </summary>
        Ping = 1
    }

    public enum ServerToSocketEvents
    {
        /// <summary>
        /// Response of ping
        /// </summary>
        Pong = 1000,

        Init = 1001,
    }


}