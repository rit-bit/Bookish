using System.Collections;
using System.Collections.Generic;
using Bookish.DatabaseInterfaces;

namespace Bookish.Models
{
    public class UsersModel
    {
        public IEnumerable<User> users { get; set; }
        
        public string FormatBalance(decimal balance)
        {
            return $"£{balance:0.00}";
        }
    }
}