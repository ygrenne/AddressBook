using System;
using AddressBook.Logging;
using AddressBook.Models;
using AddressBook.Models.Enums;

namespace AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var addressBook = new Models.AddressBook();

            addressBook.UserAdded += OnUserChange;
            addressBook.UserRemoved += OnUserChange;

            var logger = LoggerFactory.Default.GetLogger(nameof(Models.AddressBook));

            foreach (var user in randomUsers)
            {
                try
                {
                    addressBook.AddUser(user);
                }
                catch (ArgumentException ex)
                {
                    logger.Error(ex.ToString());
                }
            }

            foreach (var user in randomUsers)
            {
                try
                {
                    if (!addressBook.RemoveUser(user))
                    {
                        logger.Warning($"User {user} was not removed. Please check that user was added before removing.");
                    }
                }
                catch (ArgumentException ex)
                {
                    logger.Error(ex.ToString());
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static void OnUserChange(object sender, UserEventArgs userEventArgs)
        {
            var logger = LoggerFactory.Default.GetLogger(nameof(Models.AddressBook));

            logger.Info($"User {userEventArgs.User} Was {userEventArgs.ChangeType}");
        }

        private static readonly User[] randomUsers =
        {
            new User
            {
                Address = "ul. Piotrkowska 71",
                BirthDate = DateTime.Today.AddYears(-26),
                City = "Łódź",
                Email = "ygrenne@gmail.com",
                FirstName = "Roman",
                LastName = "Konyk",
                Gender = Gender.Male,
                PhoneNumber = "+380966541922",
                TimeAdded = DateTimeOffset.Now
            },
            new User
            {
                Address = "182 Highland View Drive, CA 95628",
                BirthDate = DateTime.Today.AddYears(-33),
                City = "Fair Oaks",
                Email = "BethanyRHereford@inbound.plus",
                FirstName = "Vera J.",
                LastName = "Streeter",
                Gender = Gender.Female,
                PhoneNumber = "916-638-3618",
                TimeAdded = DateTimeOffset.Now
            },
            new User
            {
                Address = "Via Spalato, 80",
                BirthDate = DateTime.Today.AddYears(-75),
                City = "Sumirago",
                Email = "BethanyRHereford@inbound.plus",
                FirstName = "Julie",
                LastName = "McCleary",
                Gender = Gender.Female,
                PhoneNumber = "0327 8386500",
                TimeAdded = DateTimeOffset.Now
            }
        };
    }
}
