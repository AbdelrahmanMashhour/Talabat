using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Product_Specs
{
    public class ProductWithFiltersForCountSpecifications : BaseSpecifications<Product>
    {
        public ProductWithFiltersForCountSpecifications(ProductSpecParams productParams)
            : base(p =>
                (string.IsNullOrEmpty(productParams.Search) || p.Name.Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || p.BrandId == productParams.BrandId) &&
                (!productParams.CategoryId.HasValue || p.CategoryId == productParams.CategoryId))
        { }
    }
}
