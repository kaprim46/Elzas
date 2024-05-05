using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification(ProductSpecParams productParams)
               : base(q => 
                  (string.IsNullOrEmpty(productParams.Search) || q.Name.ToLower().Contains(productParams.Search)) &&
                  (!productParams.BrandId.HasValue || q.ProductBrandId == productParams.BrandId) &&
                  (!productParams.TypeId.HasValue || q.ProductTypeId == productParams.TypeId)
               )
        {
            AddInclude(q => q.ProductType);
            AddInclude(q => q.ProductBrand);
            AddOrderBy(q => q.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
            
            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                      AddOrderBy(q => q.Price);
                      break;
                    case "priceDesc":
                      AddOrderByDescending(q => q.Price);
                      break;
                    default:
                      AddOrderBy(q => q.Name);
                      break;
                }
            }
        }

        public ProductWithTypesAndBrandsSpecification(int id) : base(q => q.Id == id)
        {
            AddInclude(q => q.ProductType);
            AddInclude(q => q.ProductBrand);
        }
    }
}