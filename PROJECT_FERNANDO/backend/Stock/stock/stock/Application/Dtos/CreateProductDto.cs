using System.ComponentModel.DataAnnotations;

namespace stock.Application.Dtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "O código é obrigatório.")]
        [StringLength(50, ErrorMessage = "O código deve ter no máximo 50 caracteres.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        public string Description { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
        public int Stock { get; set; }
    }
}
