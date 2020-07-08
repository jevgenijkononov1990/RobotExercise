using System;
using System.Runtime.CompilerServices;
using Robot.Common.Logging.Extensions;

namespace Robot.Common.Logging
{
    public class Logger : ILogger
    {
        private readonly log4net.ILog _logger;

        public Logger(Type type)
        {
            _logger = log4net.LogManager.GetLogger(type);
        }

        public void Starting(string message = "", [CallerMemberName] string memberName = "")
        {
            if (_logger.IsDebugEnabled)
            {
                _logger.Starting(message, memberName);
            }
        }

        public void Exiting(string message, [CallerMemberName] string memberName = "")
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Exiting(message, memberName);
            }
        }

        public void Exiting(LogLevel level, string message, [CallerMemberName] string memberName = "")
        {
            if (IsEnabled(level))
            {
                _logger.Exiting(level, message, memberName);
            }
        }

        public void Message(string message, [CallerMemberName] string memberName = "")
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Message(message, memberName);
            }
        }

        public void Message(LogLevel level, string message, [CallerMemberName] string memberName = "")
        {
            if (IsEnabled(level))
            {
                _logger.Message(level, message, memberName);
            }
        }

        public void Finishing(string message = "", [CallerMemberName] string memberName = "")
        {
            if (_logger.IsDebugEnabled)
            {
                _logger.Finishing(message, memberName);
            }
        }

        public void Exception(string message, Exception ex, [CallerMemberName] string memberName = "")
        {
            _logger.Exception(message, ex, memberName);
        }

        public void Exception(Exception ex, [CallerMemberName] string memberName = "")
        {
            _logger.Exception(ex, memberName);
        }

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return _logger.IsDebugEnabled;
                case LogLevel.Info:
                    return _logger.IsInfoEnabled;
                case LogLevel.Warn:
                    return _logger.IsWarnEnabled;
                case LogLevel.Error:
                    return _logger.IsErrorEnabled;
                case LogLevel.Fatal:
                    return _logger.IsFatalEnabled;
                default:
                    return false;
            }
        }
    }
}
