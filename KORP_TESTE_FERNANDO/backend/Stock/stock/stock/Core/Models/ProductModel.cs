namespace stock.core.model
{
    public class ProductModel
    {
        public Guid Id { get; set; }    
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }

    }
}
