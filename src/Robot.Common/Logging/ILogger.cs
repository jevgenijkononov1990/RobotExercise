using System;
using System.Runtime.CompilerServices;

namespace Robot.Common.Logging
{
    public interface ILogger
    {
        void Starting(string message = "", [CallerMemberName] string memberName = "");
        void Exiting(string message, [CallerMemberName] string memberName = "");
        void Exiting(LogLevel level, string message, [CallerMemberName] string memberName = "");
        void Message(string message, [CallerMemberName] string memberName = "");
        void Message(LogLevel level, string message, [CallerMemberName] string memberName = "");
        void Finishing(string message = "", [CallerMemberName] string memberName = "");
        void Exception(string message, Exception ex, [CallerMemberName] string memberName = "");
        void Exception(Exception ex, [CallerMemberName] string memberName = "");

        bool IsEnabled(LogLevel level);
    }
}
