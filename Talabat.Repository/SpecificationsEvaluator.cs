using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationsEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecifications<T> specifications)
        {
            var query = inputQuery; //_dbContext.Set<T>();

            if (specifications.Criteria is not null) //p => p.Id == id
            {
                query.Where(specifications.Criteria);
            }
            //query = query.Set<T>().Where(p=>p.Id==id);


            query=specifications.Includes.Aggregate(query,(currentQuery,includeExpression) => currentQuery.Include(includeExpression));

            /*
            var names = new[] { "Ahmed", "Nasr", "Eldine" };
            var message = "Hello";
            message = names.Aggregate(message, (str01, str02) => $"{str01} {str01}");
             */


            return query;
        }

    }
}
