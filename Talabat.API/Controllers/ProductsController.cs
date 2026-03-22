using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs.Respons;
using Talabat.Core.Entities;
using Talabat.Core.Product_Specs;
using Talabat.Core.Repositories.Contracts;
using Talabat.Core.Specifications;

namespace Talabat.API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IMapper mapper)
        {
            _productsRepo = productsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();
            var results = await _productsRepo.GetAllWithSpecAsync(spec);

            if(results == null || !results.Any())
                return NotFound("No products found.");
            return Ok(_mapper.Map<IEnumerable<Product>,IEnumerable<ProductResponseDTO>>(results));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var result = await _productsRepo.GetWithSpecAsync(spec);
            if (result == null) return NotFound();
            return Ok(_mapper.Map<Product, ProductResponseDTO>(result));
        }
    }
}
