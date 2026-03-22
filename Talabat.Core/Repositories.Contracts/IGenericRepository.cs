using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetWithSpecAsync(ISpecifications<T> spec);
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

    }
}
