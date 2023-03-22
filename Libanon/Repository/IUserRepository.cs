using Libanon.Models;


namespace Libanon.Repository
{
    public interface IUserRepository
    {
        User GetUserWithMail(string email);
        User AddNew(User user);
        User Update(User user);
        bool Delete(string id);
    }
}
