using System.ComponentModel.DataAnnotations;

namespace billing.Application.Dtos
{
    public class CreateInvoiceItemDto
    {
        [Required]
        public Guid ProductId  { get; set; }
        [Required(ErrorMessage = "O código do produto é obrigatório.")]
        public string ProductCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
        public string ProductDescription { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantity { get; set; }
    }
}
