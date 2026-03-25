using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs.Respons;
using Talabat.API.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Product_Specs;
using Talabat.Core.Repositories.Contracts;
using Talabat.Core.Specifications;

namespace Talabat.API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepository;
        private readonly IGenericRepository<ProductCategory> _categoriesRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IMapper mapper, IGenericRepository<ProductBrand> brandsRepository, IGenericRepository<ProductCategory> categoriesRepository)
        {
            _productsRepo = productsRepo;
            _mapper = mapper;
            _brandsRepository = brandsRepository;
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]ProductSpecParams productParam)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(productParam);
            var results = await _productsRepo.GetAllWithSpecAsync(spec);

            if (results == null || !results.Any())
                return NotFound("No products found.");
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponseDTO>>(results));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var result = await _productsRepo.GetWithSpecAsync(spec);
            if (result == null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product, ProductResponseDTO>(result));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands()
        {
            var brands = await _brandsRepository.GetAllAsync();
            if (brands == null || !brands.Any())
                return NotFound("No brands found.");
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            var categories = await _categoriesRepository.GetAllAsync();
            if (categories == null || !categories.Any())
                return NotFound("No categories found.");
            return Ok(categories);
        }
    }
}
