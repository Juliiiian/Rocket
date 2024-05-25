using System;

namespace Oxide.Ext.Rocket.Helpers
{
    /// <summary>
    /// Helper methods relating to time
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// DateTime since linux epoch
        /// </summary>
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        /// <summary>
        /// Gets how many seconds since the linux epoch
        /// </summary>
        /// <returns>Seconds since linux epoch</returns>
        public static int TimeSinceEpoch() => (int)(DateTime.UtcNow - Epoch).TotalSeconds;
        
        /// <summary>
        /// Gets the time since the linux epoch and the given date time
        /// </summary>
        /// <param name="time">DateTime to get total second for</param>
        /// <returns>Total seconds since linux epoch for date time</returns>
        public static double TimeSinceEpoch(DateTime time) => (time - Epoch).TotalSeconds;
        
        /// <summary>
        /// Converts the seconds since linux epoch to a DateTime
        /// </summary>
        /// <returns></returns>
        public static DateTime ToDateTime(this int timestamp) => Epoch.AddSeconds(timestamp);
        
        /// <summary>
        /// Gets the time since the linux epoch and the given date time
        /// </summary>
        /// <param name="date">DateTime to get total second for</param>
        /// <returns>Total seconds since linux epoch for date time</returns>
        public static int ToUnixTimeStamp(this DateTime date) => (int) (date - Epoch).TotalSeconds;
    }
}
