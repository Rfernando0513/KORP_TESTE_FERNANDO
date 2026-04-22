using System.ComponentModel.DataAnnotations;

namespace stock.Application.Dtos
{
    public class UpdateProductDto
    {
        [StringLength(50, ErrorMessage = "O código deve ter no máximo 50 caracteres.")]
        public string? Code { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
        public int? Stock { get; set; }
    }
}
