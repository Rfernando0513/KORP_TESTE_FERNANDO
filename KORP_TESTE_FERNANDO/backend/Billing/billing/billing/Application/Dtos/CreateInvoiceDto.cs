using System.ComponentModel.DataAnnotations;

namespace billing.Application.Dtos
{
    public class CreateInvoiceDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "A nota deve possuir ao menos um item.")]
        public List<CreateInvoiceItemDto> Items { get; set; } = new();
    }
}
