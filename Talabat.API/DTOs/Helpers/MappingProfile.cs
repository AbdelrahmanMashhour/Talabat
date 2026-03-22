using AutoMapper;
using Talabat.API.DTOs.Respons;
using Talabat.Core.Entities;
using static System.Net.WebRequestMethods;

namespace Talabat.API.DTOs.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponseDTO>()
                .ForMember(d => d.Brand, options => options.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, options => options.MapFrom(s => s.Category.Name))
                //.ForMember(d=>d.PictureUrl,options=>options.MapFrom(s=>$"{"https://localhost:7092"}/{s.PictureUrl}"));
                .ForMember(d => d.PictureUrl, options => options.MapFrom< ProductPictureUrlResolver>());
        }
    }
}
