using System;

namespace AddressBook.Logging
{
    public class ConsoleLoggWritter : ILoggWritter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}