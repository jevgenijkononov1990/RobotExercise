using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using log4net;

namespace Robot.Common.Logging.Extensions
{
    public static class Log4NetExtensions
    {
        public static void Starting(this ILog log, string message = "", [CallerMemberName] string memberName = "")
        {
            Task.Run(() => log.Debug($"{memberName} | Starting {(!string.IsNullOrWhiteSpace(message) ? "| " + message : "")}"));
        }

        public static void Exiting(this ILog log, string message, [CallerMemberName] string memberName = "")
        {
            Task.Run(() => log.Info($"{memberName} | Exiting | {message}"));
        }

        public static void Exiting(this ILog log, LogLevel level, string message, [CallerMemberName] string memberName = "")
        {
            switch (level)
            {
                case LogLevel.Debug:
                    Task.Run(() => log.Debug($"{memberName} | Exiting | {message}"));
                    break;
                case LogLevel.Info:
                    Task.Run(() => log.Info($"{memberName} | Exiting | {message}"));
                    break;
                case LogLevel.Warn:
                    Task.Run(() => log.Warn($"{memberName} | Exiting | {message}"));
                    break;
                case LogLevel.Error:
                    Task.Run(() => log.Error($"{memberName} | Exiting | {message}"));
                    break;
                case LogLevel.Fatal:
                    Task.Run(() => log.Fatal($"{memberName} | Exiting | {message}"));
                    break;
                default:
                    Task.Run(() => log.Info($"{memberName} | Exiting | {message}"));
                    break;
            }
        }

        public static void Message(this ILog log, string message, [CallerMemberName] string memberName = "")
        {
            Task.Run(() => log.Info($"{memberName} | Message | {message}"));
        }

        public static void Message(this ILog log, LogLevel level, string message, [CallerMemberName] string memberName = "")
        {
            switch (level)
            {
                case LogLevel.Debug:
                    Task.Run(() => log.Debug($"{memberName} | Message | {message}"));
                    break;
                case LogLevel.Info:
                    Task.Run(() => log.Info($"{memberName} | Message | {message}"));
                    break;
                case LogLevel.Warn:
                    Task.Run(() => log.Warn($"{memberName} | Message | {message}"));
                    break;
                case LogLevel.Error:
                    Task.Run(() => log.Error($"{memberName} | Message | {message}"));
                    break;
                case LogLevel.Fatal:
                    Task.Run(() => log.Fatal($"{memberName} | Message | {message}"));
                    break;
                default:
                    Task.Run(() => log.Info($"{memberName} | Message | {message}"));
                    break;
            }
        }

        public static void Finishing(this ILog log, string message = "", [CallerMemberName] string memberName = "")
        {
            Task.Run(() => log.Debug($"{memberName} | Finishing {(!string.IsNullOrWhiteSpace(message) ? "| " + message : "")}"));
        }

        public static void Exception(this ILog log, string message, Exception ex, [CallerMemberName] string memberName = "")
        {
            Task.Run(() => log.Error($"{memberName} | Exception | {message} | Exception details: {ex?.Message ?? "Null Exception"} {(ex?.InnerException != null ? ex.InnerException.Message : "")}", ex));
        }

        public static void Exception(this ILog log, Exception ex, [CallerMemberName] string memberName = "")
        {
            Task.Run(() => log.Error($"{memberName} | Exception | - | Exception details: {ex?.Message ?? "Null Exception"} {(ex?.InnerException != null ? ex.InnerException.Message : "")}", ex));
        }
    }
}
