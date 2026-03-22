using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contracts;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T?> GetAsync(int id)
        {
            //if (typeof(T) == typeof(Product))
            //{
            //    return await _dbContext.Set<Product>().Where(p=>p.Id==id).Include(p => p.Brand).Include(c => c.Category).FirstOrDefaultAsync() as T;
            //}
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //if (typeof(T) == typeof(Product))
            //{
            //    return (IEnumerable<T>)await _dbContext.Set<Product>().Include(p=>p.Brand).Include(c=>c.Category).ToListAsync();
            //}
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplaySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplaySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplaySpecification(ISpecifications<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }
    }
}
