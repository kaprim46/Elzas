using System.Text.Json;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if(!db.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                db.ProductBrands.AddRange(brands);
            }

            if(!db.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                db.ProductTypes.AddRange(types);
            }

            if(!db.Products.Any())
            {
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                db.Products.AddRange(products);
            }

            if(!db.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                db.DeliveryMethods.AddRange(methods);
            }

            if(db.ChangeTracker.HasChanges()) await db.SaveChangesAsync();
        }
    }
}