﻿using WebApp.Observer.Models;

namespace WebApp.Observer.Observer
{
    public class UserObserverCreateDiscount : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverCreateDiscount(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser user)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();

            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();  // sayesinde AppIdentityDbContext sınıfı üzerinden veritabanı işlemleri gerçekleştirilebilir

            context.Discounts.Add(new Discount
            {
                UserId = user.Id,
                Rate = 10
            });

            context.SaveChanges();

            logger.LogInformation($"Discount created for user: {user.UserName}");

        }
    }
}
