using Firebase.Auth;
using System.Threading.Tasks;

namespace Libanon.Repository
{
    public interface IAuthFirebaseService
    {
        Task<FirebaseAuthLink> RegisterMail(string mail, string pwd, string displayName);
        Task<FirebaseAuthLink> LoginMail(string mail, string pwd);

    }
}
