using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contracts;

namespace Talabat.API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;

        public ProductsController(IGenericRepository<Product> productsRepo)
        {
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var results = await _productsRepo.GetAllAsync();
            if(results == null || !results.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(results);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var result = await _productsRepo.GetAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
