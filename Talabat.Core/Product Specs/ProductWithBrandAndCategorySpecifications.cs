using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecifications(string sort,int? brandId,int? categoryId):base(p=>
                        (!brandId.HasValue || p.BrandId == brandId)&&
                        (!categoryId.HasValue || p.CategoryId == categoryId)
        )
        {
            AddIncludes();
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductWithBrandAndCategorySpecifications(int id):base(p=>p.Id==id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
