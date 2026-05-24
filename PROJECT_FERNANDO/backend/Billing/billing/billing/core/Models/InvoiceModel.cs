using billing.core.Enums;

namespace billing.core.Models
{
    public class InvoiceModel
    {
        public Guid Id { get; set; }
        public int SequentialNumber { get; set; }
        public InvoiceStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<InvoiceItemModel> Items { get; set; } = new();
    }
}
