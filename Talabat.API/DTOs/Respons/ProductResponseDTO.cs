using Talabat.Core.Entities;

namespace Talabat.API.DTOs.Respons
{
    public class ProductResponseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        // relations between entities 

        public int BrandId { get; set; }
        public string Brand { get; set; }

        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}
