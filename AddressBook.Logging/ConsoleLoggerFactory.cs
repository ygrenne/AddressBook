using System.Collections.Generic;

namespace AddressBook.Logging
{
    public class ConsoleLoggerFactory : LoggerFactory
    {
        private static LoggerFactory instance;
        private static readonly IDictionary<string, ILogger> loggers = new Dictionary<string, ILogger>();

        public static LoggerFactory Instance => instance ?? (instance = new ConsoleLoggerFactory());

        private ConsoleLoggerFactory()
        {
        }

        public override ILogger GetLogger(string name)
        {
            ILogger logger;
            if (loggers.TryGetValue(name, out logger))
            {
                return logger;
            }

            logger = new Logger(new ConsoleLoggWritter())
            {
                Name = name
            };

            loggers.Add(name, logger);

            return logger;
        }
    }
}