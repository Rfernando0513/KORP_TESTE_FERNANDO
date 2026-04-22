using billing.Application.Dtos;
using global::billing.core.Services;
using Microsoft.AspNetCore.Mvc;

namespace billing.Application.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _service;

        public InvoiceController(InvoiceService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceDto dto)
        {
            try
            {
                var invoice = await _service.CreateInvoice(dto);
                return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _service.GetAll();
            return Ok(invoices);
        }

        [HttpGet("(GetById/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var invoice = await _service.GetById(id);

            if (invoice == null)
                return NotFound(new { message = "Nota fiscal não encontrada." });

            return Ok(invoice);
        }
    }
}