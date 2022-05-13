namespace Catalog.Api.Data.Seeds
{
    public static class CatalogDbContextSeed
    {
        public static void SeedData(IMongoCollection<Product> products)
        {
            bool existsProduct = products.Find(_ => true).Any();

            if (!existsProduct)
            {
                List<Product> productToAdd = new()
                {
                    new Product
                    {
                        Title = "IPHONE 13",
                        Price = 1000,
                        Category = "Smart Phone",
                        Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                        ImagePath = "iphone_13.jpg"
                    },
                    new Product
                    {
                        Title = "IPHONE 12",
                        Price = 999,
                        Category = "Smart Phone",
                        Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                        ImagePath = "iphone_12.jpg"
                    },
                    new Product
                    {
                        Title = "IPHONE 11",
                        Price = 899,
                        Category = "Smart Phone",
                        Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                        ImagePath = "iphone_11.jpg"
                    }
                };
                products.InsertManyAsync(productToAdd);
            }
        }
    }
}
