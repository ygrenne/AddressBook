using System;
using AddressBook.Logging.Enums;

namespace AddressBook.Logging
{
    public class Logger : ILogger
    {
        private const string TimeStampFormat = "hh\\:mm\\:ss\\.fff";

        private readonly ILoggWritter writter;

        public LogLevel LogLevel { get; } = LogLevel.Info;
        public string Name { get; set; }

        public Logger(ILoggWritter writter)
        {
            this.writter = writter;
        }

        public void Info(string message)
        {
            if (LogLevel.HasFlag(LogLevel.Info))
            {
                LogMessage(LogLevel.Info, message);
            }
        }

        public void Debug(string message)
        {
            if (LogLevel.HasFlag(LogLevel.Debug))
            {
                LogMessage(LogLevel.Debug, message);
            }
        }

        public void Warning(string message)
        {
            if (LogLevel.HasFlag(LogLevel.Warning))
            {
                LogMessage(LogLevel.Warning, message);
            }
        }

        public void Error(string message)
        {
            if (LogLevel.HasFlag(LogLevel.Error))
            {
                LogMessage(LogLevel.Error, message);
            }
        }

        private void LogMessage(LogLevel level, string message)
        {
            writter.Write($"{level}[{DateTime.Now.TimeOfDay.ToString(TimeStampFormat)}]: {message}");
        }
    }
}
