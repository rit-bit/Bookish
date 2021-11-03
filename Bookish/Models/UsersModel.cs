using System.Collections;
using System.Collections.Generic;
using Bookish.DatabaseInterfaces;

namespace Bookish.Models
{
    public class UsersModel
    {
        public IEnumerable<UserModel> users { get; set; }
        
        public string FormatBalance(decimal balance)
        {
            return $"£{balance:0.00}";
        }
    }
}