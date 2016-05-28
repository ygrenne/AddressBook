using System;
using AddressBook.Models.Enums;

namespace AddressBook.Models
{
    public class User
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public DateTimeOffset TimeAdded { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
