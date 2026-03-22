using AutoMapper;
using Talabat.API.DTOs.Respons;
using Talabat.Core.Entities;

namespace Talabat.API.DTOs.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductResponseDTO>()
                .ForMember(d=>d.Brand,options=>options.MapFrom(s=>s.Brand.Name))
                .ForMember(d=>d.Category,options=>options.MapFrom(s=>s.Category.Name));
        }
    }
}
