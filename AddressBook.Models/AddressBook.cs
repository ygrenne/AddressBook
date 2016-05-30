using System;
using System.Collections.Generic;
using System.Linq;
using AddressBook.Models.Enums;
using static System.StringComparison;

namespace AddressBook.Models
{
    public class AddressBook
    {
        private readonly List<User> users = new List<User>();

        public event EventHandler<UserEventArgs> UserAdded;
        public event EventHandler<UserEventArgs> UserRemoved;

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            users.Add(user);

            UserAdded?.Invoke(this, new UserEventArgs(user, UserChangeType.Added));
        }

        public bool RemoveUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (users.Remove(user))
            {
                UserRemoved?.Invoke(this, new UserEventArgs(user, UserChangeType.Removed));

                return true;
            }

            return false;
        }

        public IEnumerable<User> GmailUsers()
        {
            return users.Where(u => u.Email.EndsWith("@gmail.com", OrdinalIgnoreCase));
        }

        public IEnumerable<User> RecentlyAddedFemaleUsers()
        {
            return from user in users
                   where (user.Gender == Gender.Woman &&
                          user.TimeAdded > DateTimeOffset.Now.AddDays(-10))
                   select user;
        }

        public IEnumerable<User> JanuaryUsers()
        {
            return users.Where(u => u.BirthDate.Month == 1 &&
                                    !string.IsNullOrEmpty(u.Address) &&
                                    !string.IsNullOrEmpty(u.PhoneNumber))
                        .OrderByDescending(u => u.LastName);
        }

        public IDictionary<string, List<User>> GenderToUsersDictionary()
        {
            return users.GroupBy(u => u.Gender).ToDictionary(u => u.Key.ToString().ToLower(), u => u.ToList());
        }

        public int UsersBirthdayTodayCount(string city)
        {
            return (from user in users
                    where string.Compare(user.City, city, OrdinalIgnoreCase) == 0 &&
                          (user.BirthDate.Month == DateTime.Now.Month) &&
                          (user.BirthDate.Day == DateTimeOffset.Now.Day)
                    select user).Count();
        }

        public IEnumerable<User> Page(Func<User, bool> predicate, int from, int to)
        {
            return users.Where(predicate).Skip(from).Take(to);
        }

        public override string ToString()
        {
            return string.Join("; ", users);
        }
    }
}