namespace AddressBook.Logging
{
    public abstract class LoggerFactory
    {
        public static LoggerFactory Default => ConsoleLoggerFactory.Instance;

        public abstract ILogger GetLogger(string name);
    }
}