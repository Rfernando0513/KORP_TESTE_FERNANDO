using Microsoft.AspNetCore.Mvc;
using stock.Application.Dtos;
using stock.Core.Services;

namespace stock.Application.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            try
            {
                var createdProduct = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAll();
            return Ok(products);
        }

        [HttpGet("GetById/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _service.GetById(id);

            if (product == null)
                return NotFound(new { message = "Produto não encontrado" });

            return Ok(product);
        }

        [HttpPatch("Update/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                var product = await _service.Update(id, dto);

                if (product == null)
                    return NotFound(new { message = "Produto não encontrado" });

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
