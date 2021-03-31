using System;
using System.Collections.Generic;
using System.Linq;
using libraryVueApp.Model;
using libraryVueApp.Security;
using Microsoft.EntityFrameworkCore;

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

        public void AssignRole(User user, RoleType role)
        {
            _libraryContext.UserRoles.Add(new UserRole
            {
                RoleId = role,
                UserId = user.Id
            });
        }

        public void Delete(User user)
        {
            _libraryContext.Users.Remove(user);
        }

        public IEnumerable<User> GetAllUsers(bool includeRoles = false)
        {
            if (includeRoles)
                return _libraryContext.Users.Include(u => u.Roles);
            return _libraryContext.Users.ToList();
        }

        public User GetUserById(int userId)
        {
            return _libraryContext.Users.Find(userId);
        }

        public User GetUserByLogin(string login)
        {
            return _libraryContext.Users
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Login == login);                
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
            _libraryContext.SaveChanges();
            return true;
        }

        public bool Single(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
