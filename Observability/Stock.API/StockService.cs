using Common.Shared.DTOs;
using Stock.API.Services;
using System.Diagnostics;

namespace Stock.API
{
    public class StockService
    {
        private readonly PaymentService _paymentService;
        private readonly ILogger<StockService> _logger;

        public StockService(PaymentService paymentService, ILogger<StockService> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        private Dictionary<int,int> GetProductStockList()
        {
            Dictionary<int, int> productStockList = new Dictionary<int, int>();
            productStockList.Add(1, 10);
            productStockList.Add(2, 20);
            productStockList.Add(3, 30);

            return productStockList;
        }

        public async Task<ResponseDto<StockCheckAndPaymentProcessResponseDto>> CheckAndPaymentProcess(StockCheckAndPaymentProcessRequestDto requestDto)
        {
            Activity.Current?.GetBaggageItem("UserId"); // baggage sayesinde tüm activitylerde UserId'yi görebiliriz

            var productStockList = GetProductStockList();

            var stockStatus = new List<(int productId, bool stockExist)>(); // tuple list 

            foreach (var requestItem in requestDto.OrderItems)
            { 
                var hasExistStock = productStockList.Any(x=>x.Key == requestItem.ProductId && x.Value >= requestItem.Count);

                stockStatus.Add((requestItem.ProductId, hasExistStock));

            }

            if(stockStatus.Any(x=>x.stockExist == false))
            {
                return ResponseDto<StockCheckAndPaymentProcessResponseDto>.Fail(400, "Stock not exist");
            }
            _logger.LogInformation("Stock check is success{@orderCode}",requestDto.OrderCode);
            var (isSuccess,failMessage) = await _paymentService.CreatePaymentProcess(new PaymentCreateRequestDto
            {
                OrderCode = requestDto.OrderCode,
                TotalPrice = requestDto.OrderItems.Sum(x => x.Price * x.Count)
            });

            if (!isSuccess)
            {
                return ResponseDto<StockCheckAndPaymentProcessResponseDto>.Fail(400, failMessage!);
            }

            return ResponseDto<StockCheckAndPaymentProcessResponseDto>.Success(200, new StockCheckAndPaymentProcessResponseDto {Description="Ödeme tamamlandı" });


        }
    }
}
