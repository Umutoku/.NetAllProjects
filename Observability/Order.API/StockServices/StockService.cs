using Common.Shared.DTOs;

namespace Order.API.StockServices
{
    public class StockService
    {
        private readonly HttpClient _httpClient;

        public StockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool isSuccess,string? failMessage)> CheckStockAndPaymentStart(StockCheckAndPaymentProcessRequestDto requestDto)
        {
            var response = await _httpClient.PostAsJsonAsync<StockCheckAndPaymentProcessRequestDto>("api/Stock/CheckAndPaymentStart",requestDto);

            if (response.IsSuccessStatusCode)
            {
                var responseDto = await response.Content.ReadFromJsonAsync<ResponseDto<StockCheckAndPaymentProcessResponseDto>>();

                if (responseDto is not null && responseDto.Errors == null)
                {
                    return (true, null); // sayesinde 200 döndüğünde failMessage olarak null dönecek
                }
                else
                {
                    return (false, responseDto!.Errors!.FirstOrDefault()); // sayesinde 400 döndüğünde failMessage olarak errors içindeki ilk hatayı dönecek
                }
            }
            else
            {
                return (false, response.ReasonPhrase); // sayesinde 500 döndüğünde failMessage olarak reasonPhrase dönecek
            }
        }
    }
}
