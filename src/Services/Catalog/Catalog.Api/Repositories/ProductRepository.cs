using Catalog.Api.Data.Interfaces;
using Catalog.Api.Repositories.Interfaces;

namespace Catalog.Api.Repositories;

public class ProductRepository : IProductRepository
{
    #region ctor

    private readonly ICatalogDbContext _context;

    public ProductRepository(ICatalogDbContext context)
    {
        _context = context;
    }

    #endregion

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var res = await _context.Products
                .FindAsync(_ => true);

        return await res.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
        var filter = Builders<Product>.Filter.ElemMatch(p => p.Category, categoryName);

        var res = await _context.Products
               .FindAsync(filter);

        return await res.ToListAsync();
    }

    public async Task<Product> GetProductById(string id)
    {
        var res = await _context.Products
               .FindAsync(p => p.Id == id);

        return await res.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string title)
    {
        var filter = Builders<Product>.Filter.ElemMatch(p => p.Title, title);

        var res = await _context.Products
               .FindAsync(filter);

        return await res.ToListAsync();
    }

    public async Task CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var res = await _context.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

        return res.IsAcknowledged && res.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        var res = await _context.Products.DeleteOneAsync(filter);

        return res.IsAcknowledged && res.DeletedCount > 0;
    }


}

