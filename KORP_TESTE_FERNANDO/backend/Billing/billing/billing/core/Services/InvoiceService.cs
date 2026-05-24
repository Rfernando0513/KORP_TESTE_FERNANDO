using AutoMapper;
using billing.Application.Dtos;
using billing.Application.Dtos.Stock;
using billing.Application.Integrations.Stock;
using billing.core.Enums;
using billing.core.Models;
using billing.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace billing.core.Services
{
    public class InvoiceService
    {
        private readonly BillingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStockServiceClient _stockServiceClient;

        public InvoiceService(BillingDbContext context,
                                IMapper mapper,
                                IStockServiceClient stockServiceClient)
        {
            _context = context;
            _mapper = mapper;
            _stockServiceClient = stockServiceClient;
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

        public async Task Print(Guid id)
        {
            var invoice = await _context.InvoiceModel
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (invoice == null)
                throw new Exception("Nota fiscal não encontrada");

            if (invoice.Status != Enums.InvoiceStatus.Open)
                throw new Exception("Apenas notas com status Open podem ser impressas");

            var stockRequest = new StockDecreaseDto
            {
                Items = invoice.Items.Select(item => new StockDecreaseItemRequestDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList()
            };

            await _stockServiceClient.DecreaseStock(stockRequest);

            invoice.Status = InvoiceStatus.Closed;

            await _context.SaveChangesAsync();
        }
    }
}
