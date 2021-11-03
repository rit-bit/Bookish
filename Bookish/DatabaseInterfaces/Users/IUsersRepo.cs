using System.Collections.Generic;
using Bookish.Models;

namespace Bookish.DatabaseInterfaces
{
    public interface IUsersRepo
    {
        IEnumerable<UserModel> GetUsers();
        bool Insert(UserModel userModel);
        bool Update(UserModel userModel);
        bool Delete(UserModel userModel);
    }
}