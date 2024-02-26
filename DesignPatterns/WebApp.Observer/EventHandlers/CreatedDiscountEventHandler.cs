﻿using MediatR;
using WebApp.Observer.Events;
using WebApp.Observer.Models;

namespace WebApp.Observer.EventHandlers
{
    public class CreatedDiscountEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly AppIdentityDbContext _context;
        private readonly ILogger<CreatedDiscountEventHandler> _logger;

        public CreatedDiscountEventHandler(AppIdentityDbContext context, ILogger<CreatedDiscountEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _context.Discounts.AddAsync(new Discount
            {
                UserId = notification.AppUser.Id,
                Rate = 10
            });
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Discount created for user: {notification.AppUser.UserName}");
        }
    }
}
