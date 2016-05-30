using System;
using System.Collections.Generic;

namespace AddressBook.Models.Extensions
{
    public static class UsersExtension
    {
        public static IEnumerable<User> AdultsFromKyiv(this IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                if (user.BirthDate < DateTimeOffset.Now.Date.AddYears(-18) &&
                    string.Compare(user.City, "Kyiv", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    yield return user;
                }
            }
        }
    }
}
