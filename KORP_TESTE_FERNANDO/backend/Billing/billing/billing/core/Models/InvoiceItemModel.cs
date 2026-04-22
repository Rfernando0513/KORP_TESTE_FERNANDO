namespace billing.core.Models
{
    public class InvoiceItemModel
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public InvoiceModel Invoice { get; set; } = null;

    }
}
