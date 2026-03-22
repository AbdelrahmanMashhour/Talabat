using AutoMapper;
using Talabat.API.DTOs.Respons;
using Talabat.Core.Entities;

namespace Talabat.API.DTOs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductResponseDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductResponseDTO destination, string destMember, ResolutionContext context)
        {
            var baseUrl = _configuration["ApiBaseUrl"];

            if (string.IsNullOrEmpty(source.PictureUrl))
                return null;

            return $"{baseUrl}{source.PictureUrl}";
        }
    }
}
