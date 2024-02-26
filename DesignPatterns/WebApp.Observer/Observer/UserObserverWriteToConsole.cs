using WebApp.Observer.Models;

namespace WebApp.Observer.Observer
{
    public class UserObserverWriteToConsole : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider; // Sayesinde herhangi bir servisi kullanabiliriz. Alternatif olarak ILogger kullanılabilir. 

        public UserObserverWriteToConsole(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser user)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverWriteToConsole>>(); // UserObserverWriteToConsole tipinde bir logger alırız.

            logger.LogInformation($"User created: {user.UserName}");
        }
    }
}
