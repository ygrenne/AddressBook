using AddressBook.Models.Enums;

namespace AddressBook.Models
{
    public class UserEventArgs
    {
        public User User { get; }
        public UserChangeType ChangeType { get; }

        public UserEventArgs(User user, UserChangeType changeType)
        {
            User = user;
            ChangeType = changeType;
        }
    }
}