using System;
using Oxide.Core;

namespace Oxide.Ext.Rocket.Logging
{
    /// <summary>
    /// Represents a logger
    /// </summary>
    public class Logger : ILogger
    {
        private LogLevel _logLevel;
        
        /// <summary>
        /// Creates a new logger with the given log level
        /// </summary>
        /// <param name="logLevel">Log level of the logger</param>
        public Logger(LogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        /// <summary>
        /// Log a verbose message
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Verbose(string message)
        {
            Log(LogLevel.Verbose, message);
        }

        /// <summary>
        /// Log a Debug message
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }
        
        /// <summary>
        /// Log a Info message
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Info(string message)
        {
            Log(LogLevel.Info, message);
        }
        
        /// <summary>
        /// Log a Warning message
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Warning(string message)
        {
            Log(LogLevel.Warning, message);
        }
        
        /// <summary>
        /// Log a Error message
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Error(string message)
        {
            Log(LogLevel.Error, message);
        }

        /// <summary>
        /// Log a Exception message
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="ex">Exception to log</param>
        public void Exception(string message, Exception ex)
        {
            Log(LogLevel.Exception, message, ex);
        }

        private void Log(LogLevel level, string message, object data = null)
        {
            if (!IsLogging(level))
            {
                return;
            }

            string log = $"[WebSocket Extension] [{level.ToString()}]: {message}";
            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Warning:
                    Interface.Oxide.LogWarning(log);
                    break;
                case LogLevel.Error:
                    Interface.Oxide.LogError(log);
                    break;
                case LogLevel.Exception:
                    Interface.Oxide.LogException($"{log}\n{data}", (Exception)data);
                    break;
                default:
                    Interface.Oxide.LogInfo(log);
                    break;
            }
        }

        /// <summary>
        /// Updates the log level for the current logger
        /// </summary>
        /// <param name="level">Level to update the logger to</param>
        public void UpdateLogLevel(LogLevel level)
        {
            _logLevel = level;
        }

        /// <summary>
        /// Returns true if the logger is logging for the passed log level
        /// </summary>
        /// <param name="level">Log Level to check</param>
        /// <returns>True if the logger is logging for the given log level</returns>
        public bool IsLogging(LogLevel level)
        {
            return level >= _logLevel;
        }
    }
}