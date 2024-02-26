using WebApp.Observer.Models;

namespace WebApp.Observer.Observer
{
    public class UserObserverSubject
    {
        private readonly List<IUserObserver> _observers;

        public UserObserverSubject()
        {
            _observers = new List<IUserObserver>();
        }

        public void Attach(IUserObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IUserObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(AppUser user)
        {
            foreach (var observer in _observers) // Tüm observerlara haber veririz. 
            {
                observer.UserCreated(user);
            }
        }
    }
}
