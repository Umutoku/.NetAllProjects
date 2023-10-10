using Firebase;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.FirebaseUser;

namespace TodoApp.Platforms.Android
{
    public class FirebaseAuthAnd : IAuth
    {
        public async Task<bool> SignIn(string email, string password)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> SignUp(string username, string email, string password)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var profileUpdates = new Firebase.Auth.UserProfileChangeRequest.Builder();
                profileUpdates.SetDisplayName(username);
                var build = profileUpdates.Build();
                var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
                await user.UpdateProfileAsync(build);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool SignVerify()
        {
            throw new NotImplementedException();
        }
    }
}
