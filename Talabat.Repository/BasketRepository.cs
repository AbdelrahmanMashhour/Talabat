using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contracts;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _redis;

        public BasketRepository(IConnectionMultiplexer redis) => _redis = redis.GetDatabase();

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _redis.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _redis.StringGetAsync(basketId);
            return 
                basket.IsNullOrEmpty
                ? null
                : System.Text.Json.JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
          var createdOrUpdated=  await _redis.StringSetAsync(basket.Id, System.Text.Json.JsonSerializer.Serialize(basket), TimeSpan.FromMinutes(5));
            if (!createdOrUpdated) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
