using System.Diagnostics;
using Common.Shared.DTOs;
using Common.Shared.Events;
using MassTransit;
using OpenTelemetry.Shared;
using Order.API.Models;
using Order.API.RedisServices;
using Order.API.StockServices;

namespace Order.API.OrderServices
{
    public class OrderService
    {
        private readonly AppDbContext _dbContext;
        private readonly StockService _stockService;
        private readonly RedisService _redisService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderService> _logger;

        public OrderService(AppDbContext dbContext, StockService stockService, RedisService redisService, IPublishEndpoint publishEndpoint, ILogger<OrderService> logger)
        {
            _dbContext = dbContext;
            _stockService = stockService;
            _redisService = redisService;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<ResponseDto<OrderCreateResponseDto>> CreateAsync(OrderCreateRequestDto orderCreateRequestDto)
        {
            using (var redisActivity = ActivitySourceProvider.Source.StartActivity("RedisActivity"))
            {
                var db = _redisService.GetDb(0);
                await db.StringSetAsync("userID", orderCreateRequestDto.UserId);
                var redisValue = await db.StringGetAsync("userID");
                redisActivity?.AddEvent(new ActivityEvent("RedisActivity is start"));
                redisActivity?.SetTag("RedisValue", redisValue);
                redisActivity?.AddEvent(new ActivityEvent("RedisActivity is end"));
            }

            Activity.Current?.AddTag("AspNetIns(tag1)", "tagValue"); // add tag to general activity
            using var activity = ActivitySourceProvider.Source.StartActivity();

            var newOrder = new Order
            {
                OrderCode = Guid.NewGuid().ToString(),
                Created = DateTime.UtcNow,
                UserId = orderCreateRequestDto.UserId,
                Status = OrderStatus.Success,
                Items = orderCreateRequestDto.Items.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    Count = x.Count,
                    Price = x.Price
                }).ToList()
            };

            _dbContext.Orders.Add(newOrder);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Order created. OrderCode: {@orderCode}", newOrder.OrderCode); // @ işareti ile object olarak loglanabilir


            activity?.AddEvent(new ActivityEvent("Order process is start"));
            activity?.SetBaggage("UserId", orderCreateRequestDto.UserId.ToString()); // baggage sayesinde tüm activitylerde UserId'yi görebiliriz
            StockCheckAndPaymentProcessRequestDto stockRequest = new();

            stockRequest.OrderCode = newOrder.OrderCode;
            stockRequest.OrderItems = orderCreateRequestDto.Items;

            var (isSuccess, failMessage) = await _stockService.CheckStockAndPaymentStart(stockRequest);

            if (!isSuccess)
            {
                activity?.AddEvent(new ActivityEvent("Order process is fail"));

                return ResponseDto<OrderCreateResponseDto>.Fail(400, failMessage!);
            }

            activity?.SetTag("UserId", orderCreateRequestDto.UserId);

            activity?.AddEvent(new ActivityEvent("Order process is end"));

            return ResponseDto<OrderCreateResponseDto>.Success(200, new OrderCreateResponseDto { Id = newOrder.Id });
        }
    }
}
