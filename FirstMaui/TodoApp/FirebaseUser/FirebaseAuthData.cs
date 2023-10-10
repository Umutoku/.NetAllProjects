using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TodoApp.FirebaseUser
{
    public interface IAuth
    {
        Task<bool> SignIn(string email, string password);
        Task<bool> SignUp(string username, string email, string password);

        bool SignVerify();
    }
    public class FirebaseAuthData
    {
        

        static IAuth auth = new TodoApp.Platforms.Android.FirebaseAuthAnd();
        public static async Task<bool> SignIn(string email, string password)
        {
           return await auth.SignIn(email, password);
        }

        public static async Task<bool> SignUp(string username, string email, string password)
        {
            return await auth.SignUp(username, email, password);
        }

        public static bool SignVerify()
        {
           return auth.SignVerify();
        }
        
    }
}
