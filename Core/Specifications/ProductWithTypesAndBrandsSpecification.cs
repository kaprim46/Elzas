using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification()
        {
            AddInclude(q => q.ProductType);
            AddInclude(q => q.ProductBrand);
        }

        public ProductWithTypesAndBrandsSpecification(int id) : base(q => q.Id == id)
        {
            AddInclude(q => q.ProductType);
            AddInclude(q => q.ProductBrand);
        }
    }
}