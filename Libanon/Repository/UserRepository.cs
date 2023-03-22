using Libanon.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace Libanon.Repository
{
    public class UserRepository : IUserRepository
    {
        LibanonDbContext _DbContext;

        public UserRepository()
        {
            _DbContext = new LibanonDbContext();
        }

        public User GetUserWithMail(string mail)
        {
            if(mail == null)
            {
                throw new ArgumentNullException(nameof(mail));
            }
            var user = _DbContext.Users.Where(u => u.Mail == mail).FirstOrDefault();
            return user;
        }

        public User AddNew(User user)
        {
            if (user == null)
            {
                throw new Exception("Input user cannot be null!!");
            }
            else
            {

                _DbContext.Users.Add(user);
                _DbContext.SaveChanges();
            }

            return user;
        }

        public bool Delete(string id)
        {
            if (id == null)
            {
                throw new Exception("Id user cannot be null!!");
            }

            var result = false;
            var user = _DbContext.Users.Find(id);
            _DbContext.Users.Remove(user);
            int i = _DbContext.SaveChanges();

            if (i != 0)
            {
                result = true;
            }

            return result;
        }

        public User Update(User user)
        {
            if (user == null)
            {
                throw new Exception("Update user cannot be null!!");
            }
            else
            {
                _DbContext.Entry(user).State = EntityState.Modified;
                _DbContext.SaveChanges();
            }

            return user;
        }
    }
}