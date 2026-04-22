using AutoMapper;
using Microsoft.EntityFrameworkCore;
using stock.Application.Dtos;
using stock.core.model;
using stock.Infra.Data;

namespace stock.Core.Services
{
    public class ProductService
    {
        private readonly StockDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(StockDbContext context,
                                IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> Create(CreateProductDto dto)
        {
            var productExists = await _context.Product
                .AnyAsync(p => p.Code == dto.Code);

            if (productExists)
                throw new Exception("Já existe um produto com esse código");

            var product = _mapper.Map<ProductModel>(dto);
            product.Id = Guid.NewGuid();

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<List<ProductDto>> GetAll()
        {

            var product = await _context.Product
                .ToListAsync();

            if (product == null)
                return null;

            return _mapper.Map<List<ProductDto>>(product);
        }

        public async Task<ProductDto> GetById(Guid id){

            var product = await _context.Product
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto?> Update(Guid id, UpdateProductDto dto)
        {
            var product = await _context.Product
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            if (!string.IsNullOrWhiteSpace(dto.Code))
            {
                var codeAlreadyExists = await _context.Product
                    .AnyAsync(p => p.Code == dto.Code && p.Id != id);

                if (codeAlreadyExists)
                    throw new Exception("Já existe outro produto com esse código.");

                product.Code = dto.Code;
            }

            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                product.Description = dto.Description;
            }

            if (dto.Stock.HasValue)
            {
                if (dto.Stock.Value < 0)
                    throw new Exception("O estoque não pode ser negativo.");

                product.Stock = dto.Stock.Value;
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }
    }
}
