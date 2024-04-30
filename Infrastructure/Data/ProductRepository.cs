using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository(AppDbContext db) : IProductRepository
    {
        private readonly AppDbContext _db = db;

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            return await _db.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _db.Products
            .Include(q => q.ProductBrand)
            .Include(q => q.ProductType)
            .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _db.Products
            .Include(q => q.ProductBrand)
            .Include(q => q.ProductType)
            .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _db.ProductTypes.ToListAsync();
        }
    }
}