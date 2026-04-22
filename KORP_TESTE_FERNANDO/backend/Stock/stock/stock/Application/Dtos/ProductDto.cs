namespace stock.Application.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
