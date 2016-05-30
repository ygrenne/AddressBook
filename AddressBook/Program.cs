using System;
using AddressBook.Logging;
using AddressBook.Models;
using AddressBook.Models.Enums;
using AddressBook.Models.Extensions;

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

            logger.Info(string.Join(", ", addressBook.GmailUsers()) + ": have gmail.com domain");
            logger.Info(string.Join(", ", randomUsers.AdultsFromKyiv()) + ": adults users from Kyiv");
            logger.Info(string.Join(", ", addressBook.RecentlyAddedFemaleUsers()) + ": female users added last 10 days");
            logger.Info(string.Join(", ", addressBook.JanuaryUsers()) + ": january users with address and phone number");
            foreach (var users in addressBook.GenderToUsersDictionary())
            {
                logger.Info($"{users.Key}(s) : " + string.Join(", ", users.Value));
            }
            logger.Info(string.Join(", ", addressBook.UsersBirthdayTodayCount("Kyiv")) + ": lives in Kyiv and have birthday today");
            logger.Info(string.Join(", ", addressBook.Page(u => !string.IsNullOrEmpty(u.FirstName), 1, 2)) + ": paging");

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
                BirthDate = new DateTime(1989,12,25),
                City = "Kyiv",
                Email = "ygrenne@gmail.com",
                FirstName = "Roman",
                LastName = "Konyk",
                Gender = Gender.Man,
                PhoneNumber = "+380966541922",
                TimeAdded = DateTimeOffset.Now
            },
            new User
            {
                Address = "182 Highland View Drive, CA 95628",
                BirthDate = new DateTime(1964,1,14),
                City = "Fair Oaks",
                Email = "BethanyRHereford@inbound.plus",
                FirstName = "Vera J.",
                LastName = "Streeter",
                Gender = Gender.Woman,
                PhoneNumber = "916-638-3618",
                TimeAdded = DateTimeOffset.Now.AddDays(-12)
            },
            new User
            {
                Address = "Via Spalato, 80",
                BirthDate = new DateTime(1998,5,31),
                City = "Kyiv",
                Email = "BethanyRHereford@gmail.com",
                FirstName = "Julie",
                LastName = "McCleary",
                Gender = Gender.Woman,
                PhoneNumber = "0327 8386500",
                TimeAdded = DateTimeOffset.Now.AddDays(-5)
            }
        };
    }
}
