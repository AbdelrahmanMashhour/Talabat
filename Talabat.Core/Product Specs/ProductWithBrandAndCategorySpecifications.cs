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
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams productParam) :base(p=>
                        (string.IsNullOrEmpty(productParam.Search) || p.Name.Contains(productParam.Search)) &&
                        (!productParam.BrandId.HasValue || p.BrandId == productParam.BrandId) &&
                        (!productParam.CategoryId.HasValue || p.CategoryId == productParam.CategoryId)
        )
        {
            AddIncludes();
            if (!string.IsNullOrEmpty(productParam.Sort))
            {
                switch (productParam.Sort.ToLower())
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
                ApplyPagination((productParam.PageIndex - 1) * productParam.PageSize, productParam.PageSize);
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
