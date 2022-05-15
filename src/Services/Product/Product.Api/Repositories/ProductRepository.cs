using Product.Api.Data.Interfaces;
using Product.Api.Repositories.Interfaces;

namespace Product.Api.Repositories;

public class ProductRepository : IProductRepository
{
    #region ctor

    private readonly IProductDbContext _context;

    public ProductRepository(IProductDbContext context)
    {
        _context = context;
    }

    #endregion

    public async Task<IEnumerable<Entities.Product>> GetAllProducts()
    {
        var res = await _context.Products
            .FindAsync(_ => true);

        return await res.ToListAsync();
    }

    public async Task<IEnumerable<Entities.Product>> GetProductByCategory(string categoryName)
    {
        var filter = Builders<Entities.Product>.Filter.ElemMatch(p => p.Category, categoryName);

        var res = await _context.Products
            .FindAsync(filter);

        return await res.ToListAsync();
    }

    public async Task<Entities.Product> GetProductById(string id)
    {
        var res = await _context.Products
            .FindAsync(p => p.Id == id);

        return await res.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Entities.Product>> GetProductByName(string title)
    {
        var filter = Builders<Entities.Product>.Filter.ElemMatch(p => p.Title, title);

        var res = await _context.Products
            .FindAsync(filter);

        return await res.ToListAsync();
    }

    public async Task CreateProduct(Entities.Product product)
    {
        await _context.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Entities.Product product)
    {
        var res = await _context.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

        return res.IsAcknowledged && res.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var filter = Builders<Entities.Product>.Filter.Eq(p => p.Id, id);

        var res = await _context.Products.DeleteOneAsync(filter);

        return res.IsAcknowledged && res.DeletedCount > 0;
    }
}