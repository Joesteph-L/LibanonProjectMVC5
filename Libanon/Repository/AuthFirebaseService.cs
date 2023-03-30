using System.Threading.Tasks;
using Firebase.Auth;

namespace Libanon.Repository
{
    public class AuthFirebaseService:IAuthFirebaseService
    {
        private const string API_KEY = "AIzaSyDZFDZFl9_hbt3O-uGTLhYlDCqYCfxjFsw";

        private FirebaseAuthProvider firebaseAuthProvider;

        public AuthFirebaseService()
        {
            firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
        }
        
        

        public async Task<FirebaseAuthLink> RegisterMail(string mail, string pwd, string displayName)
        {
            var firebaseAuthLink = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(mail, pwd, displayName, true);
            
            
            return firebaseAuthLink;
        }

        public async Task<FirebaseAuthLink> LoginMail(string mail, string pwd)
        {
            var firebaseAuthLink = await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(mail, pwd);
            
            return firebaseAuthLink;
        }
    }
}