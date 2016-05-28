using System;
using System.Collections.Generic;
using AddressBook.Models.Enums;

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

        public override string ToString()
        {
            return string.Join("; ", users);
        }
    }
}