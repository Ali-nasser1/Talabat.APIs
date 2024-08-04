using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreConextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {
            if (!dbContext.ProductBrands.Any())
            {
                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DatasSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                        await dbContext.Set<ProductBrand>().AddAsync(Brand);
                }
            }

            if (!dbContext.ProductTypes.Any())
            {
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/DatasSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                        await dbContext.Set<ProductType>().AddAsync(Type);
                }
            }

            if (!dbContext.Products.Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DatasSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Products?.Count > 0)
                {
                    foreach (var Product in Products)
                        await dbContext.Set<Product>().AddAsync(Product);
                }
            }

            if(!dbContext.DeliveryMethods.Any())
            {
                var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DatasSeed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                if(DeliveryMethods?.Count > 0)
                {
                    foreach(var DeliveryMethod in DeliveryMethods)
                        await dbContext.Set<DeliveryMethod>().AddAsync(DeliveryMethod);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
