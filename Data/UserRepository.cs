using System;
using System.Collections.Generic;
using System.Linq;
using libraryVueApp.Model;
using libraryVueApp.Security;

namespace libraryVueApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryContext _libraryContext;

        public UserRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public void AddNewUser(User user)
        {
            user.Password = PasswordHasher.Hash(user.Password);
            _libraryContext.Users.Add(user);
        }

        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int userId)
        {
            throw new NotImplementedException();
        }
        public bool Login(string login, string password)
        {
            User user = _libraryContext.Users.SingleOrDefault(u => u.Login == login);
            if (user == null)
                return false;

            return PasswordHasher.Verify(password, user.Password);            
        }

        public bool LoginAlreadyExists(string login) =>
            _libraryContext.Users.SingleOrDefault(u => u.Login == login) != null;
        

        public bool SaveChanges()
        {
            return _libraryContext.SaveChanges() == 0;
        }

        public bool Single(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
