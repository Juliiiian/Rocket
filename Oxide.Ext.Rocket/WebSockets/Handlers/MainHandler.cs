using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oxide.Ext.Rocket.Logging;

namespace Oxide.Ext.Rocket.WebSockets.Handlers
{
    class MainHandler
    {
        public static void HandleSocketMessage(EventPayload<ServerToSocketEvents> payload, ILogger _logger)
        {
            try
            {
                _logger.Debug($"Event: {payload.EventName}");
                switch (payload.EventName)
                {
                    case ServerToSocketEvents.Init:
                        //GatewayResumedEvent resumed = payload.EventData.ToObject<GatewayResumedEvent>();
                        return;
                    default:
                        _logger.Debug($"Event: {payload.EventName} has no handler!");
                        return;
                }

            }
            catch (Exception ex)
            {
                _logger.Exception($"{nameof(MainHandler)}.{nameof(HandleSocketMessage)} Exception Occured. Please give error message below to Rocket Extension Authors:\n", ex);
            }
        }
    }
}
