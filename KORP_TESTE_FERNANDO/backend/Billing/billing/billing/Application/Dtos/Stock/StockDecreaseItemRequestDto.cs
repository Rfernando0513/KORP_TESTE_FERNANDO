namespace billing.Application.Dtos.Stock
{
    public class StockDecreaseItemRequestDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
