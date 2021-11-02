using System.Collections.Generic;
using Bookish.Models;

namespace Bookish.DatabaseInterfaces
{
    public interface IUsersRepo
    {
        IEnumerable<User> GetUsers();
        bool Insert(User user);
        bool Update(User user);
        bool Delete(User user);
    }
}