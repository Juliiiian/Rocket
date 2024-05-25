using System;
using Newtonsoft.Json;
using Oxide.Core.Configuration;
using Oxide.Ext.Rocket.Logging;

namespace Oxide.Ext.Rocket.Configuration
{
    /// <summary>
    /// Represents Discord Extension Config
    /// </summary>
    public class RocketConfig : ConfigFile
    {
        [JsonProperty("api_token")]
        public string ApiToken { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Constructor for config
        /// </summary>
        /// <param name="filename">Filename to use</param>
        public RocketConfig(string filename) : base(filename)
        {
            
        }

        /// <summary>
        /// Load the config file and populate it.
        /// </summary>
        /// <param name="filename"></param>
        public override void Load(string filename = null)
        {
            try
            {
                base.Load(filename);
            }
            catch (Exception ex)
            {
                RocketExtension.GlobalLogger.Error($"Failed to load config file. Generating new Config.\n{ex}");
                ApiToken = "";
                Url = "";
                Save(filename);
            }
        }
    }
}