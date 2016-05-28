using System;

namespace AddressBook.Logging.Enums
{
    [Flags]
    public enum LogLevel
    {
        Error = 0,
        Warning = 2,
        Info = 3,
        Debug = 6
    }
}