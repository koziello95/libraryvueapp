using libraryVueApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace libraryVueApp.Data
{
    public interface IUserRepository
    {
        void AddNewUser(User user);
        User GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
        void Delete(int userId);
        bool Login(string login, string password);
        bool SaveChanges();
        bool Single(Func<object, object> p);
        bool LoginAlreadyExists(string login);
        User GetUserByLogin(string login);
    }
}
