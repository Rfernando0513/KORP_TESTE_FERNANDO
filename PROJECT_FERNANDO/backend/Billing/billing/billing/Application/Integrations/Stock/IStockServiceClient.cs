using billing.Application.Dtos.Stock;

namespace billing.Application.Integrations.Stock
{
    public interface IStockServiceClient
    {
        Task DecreaseStock(StockDecreaseDto dto);
    }
}
