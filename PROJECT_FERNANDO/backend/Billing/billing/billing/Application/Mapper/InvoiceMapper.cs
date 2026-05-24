using AutoMapper;
using billing.Application.Dtos;
using billing.core.Models;

namespace billing.Application.Mapper
{
    public class InvoiceMapper : Profile
    {
        public InvoiceMapper()
        {
            CreateMap<InvoiceModel, InvoiceDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<InvoiceItemModel, InvoiceItemDto>();

            CreateMap<CreateInvoiceItemDto, InvoiceItemModel>();
        }
    }
}
