using AutoMapper;
using stock.core.model;
using stock.Application.Dtos;

namespace stock.Application.Mapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductModel, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, ProductModel>();
            CreateMap<UpdateProductDto, ProductModel>();
        }
    }
}
