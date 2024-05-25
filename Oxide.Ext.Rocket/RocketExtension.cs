using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Configuration;
using Oxide.Core.Extensions;
using Oxide.Ext.Rocket.Logging;
using Oxide.Ext.Rocket.Configuration;
using Oxide.Ext.Rocket.WebSockets;
using System.Runtime;

namespace Oxide.Ext.Rocket
{
    /// <summary>
    /// WebSocket Extension that is loaded by Oxide
    /// </summary>
    public class RocketExtension : Extension
    {
        /// <summary>
        /// Test version information if using test version
        /// </summary>
        public const string TestVersion = "Test";

        /// <summary>
        /// Discord Extension JSON Serialization settings
        /// </summary>
        internal static readonly JsonSerializerSettings ExtensionSerializeSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
        
        /// <summary>
        /// Version number of the extension
        /// </summary>
        private static readonly VersionNumber ExtensionVersion = new VersionNumber(0, 0, 1);
        
        /// <summary>
        /// Global logger for areas that aren't part of a client connection
        /// </summary>
        public static ILogger GlobalLogger;
        public Socket Socket;
        public RocketConfig RocketConfig;

        /// <summary>
        /// Constructor for the extension
        /// </summary>
        /// <param name="manager">Oxide extension manager</param>
        public RocketExtension(ExtensionManager manager) : base(manager)
        {
            
        }

        /// <summary>
        /// Name of the extension
        /// </summary>
        public override string Name => "Rocket";

        /// <summary>
        /// Authors for the extension
        /// </summary>
        public override string Author => "Juliiian";

        /// <summary>
        /// Version number used by oxide
        /// </summary>
        public override VersionNumber Version => ExtensionVersion;

        /// <summary>
        /// Gets full extension version including test information
        /// </summary>
        public static string GetExtensionVersion => ExtensionVersion.ToString() + TestVersion; 

        /// <summary>
        /// Called when mod is loaded
        /// </summary>
        public override void OnModLoad()
        {
            GlobalLogger = string.IsNullOrEmpty(TestVersion) ? new Logger(LogLevel.Warning) : new Logger(LogLevel.Debug);
            
            GlobalLogger.Info($"Using Extension Version: {GetExtensionVersion}");
            AppDomain.CurrentDomain.UnhandledException += (sender, exception) =>
            {
                GlobalLogger.Exception("An exception was thrown!", exception.ExceptionObject as Exception);
            };

            string configPath = Path.Combine(Interface.Oxide.InstanceDirectory, "websocket.config.json");
            if (!File.Exists(configPath))
            {
                RocketConfig = new RocketConfig(configPath);
                RocketConfig.Save();
            }

            RocketConfig = ConfigFile.Load<RocketConfig>(configPath);
            RocketConfig.Save();

            Interface.Call("OnWebSocketCreated", RocketConfig);
        }

        /// <summary>
        /// Called when server is shutdown
        /// </summary>
        public override void OnShutdown()
        {
            Socket.Disconnect(false, false);

            GlobalLogger.Info("Disconnected WebSocket - server shutdown.");
        }
    }
}
