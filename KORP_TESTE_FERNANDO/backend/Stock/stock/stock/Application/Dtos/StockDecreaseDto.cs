namespace stock.Application.Dtos.Stock
{
    public class StockDecreaseDto
    {
        public List<StockDecreaseItemRequestDto> Items { get; set; } = new();
    }
}
