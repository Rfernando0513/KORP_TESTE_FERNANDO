using billing.Application.Dtos.Stock;

namespace billing.Application.Integrations.Stock
{
    public class StockServiceClient : IStockServiceClient
    {
        private readonly HttpClient _httpClient;

        public StockServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DecreaseStock(StockDecreaseDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Product/decrease-stock", dto);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao atualizar estoque | Status: {(int)response.StatusCode} - {response.ReasonPhrase} | Body: {errorContent}"
);
            }
        }
    }
}
