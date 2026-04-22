using AutoMapper;
using billing.Application.Dtos;
using billing.core.Models;
using billing.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace billing.core.Services
{
    public class InvoiceService
    {
        private readonly BillingDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceService(BillingDbContext context,
                                IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InvoiceDto> CreateInvoice(CreateInvoiceDto dto)
            {
                var lastSequentialNumber = await _context.InvoiceModel
                    .OrderByDescending(i => i.SequentialNumber)
                    .Select(i => i.SequentialNumber)
                    .FirstOrDefaultAsync();

            var invoice = new InvoiceModel
            {
                Id = Guid.NewGuid(),
                SequentialNumber = lastSequentialNumber + 1,
                Status = Enums.InvoiceStatus.Open,
                CreatedAt = DateTime.UtcNow,
                Items = dto.Items.Select(item => new InvoiceItemModel
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    ProductCode = item.ProductCode,
                    ProductDescription = item.ProductDescription,
                    Quantity = item.Quantity,
                }).ToList()
            };

            _context.InvoiceModel.Add(invoice);
            await _context.SaveChangesAsync();

            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<List<InvoiceDto>> GetAll()
        {
            var invoices = await _context.InvoiceModel
                .Include(x => x.Items)
                .OrderByDescending(x => x.SequentialNumber)
                .ToListAsync();

            return _mapper.Map<List<InvoiceDto>>(invoices);
        }

        public async Task<InvoiceDto?> GetById(Guid id)
        {
            var invoice = await _context.InvoiceModel
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (invoice == null)
                return null;

            return _mapper.Map<InvoiceDto>(invoice);
        }
    }
}
