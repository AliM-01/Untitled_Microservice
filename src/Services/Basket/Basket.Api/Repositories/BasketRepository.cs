using Basket.Api.Data.Interfaces;
using Basket.Api.Entities;
using Basket.Api.Repositories.Interfaces;
using Newtonsoft.Json;

namespace Basket.Api.Repositories;

public class BasketRepository : IBasketRepository
{
    #region ctor

    private readonly IBasketContext _context;

    public BasketRepository(IBasketContext context)
    {
        _context = context;


    }

    #endregion

    public async Task<BasketCart> GetBasket(string username)
    {
        var basket = await _context.Redis.StringGetAsync(username);

        if (basket.IsNullOrEmpty)
        {
            return null;
        }

        return JsonConvert.DeserializeObject<BasketCart>(basket);
    }

    public async Task<BasketCart> UpdateBasket(BasketCart cart)
    {
        bool updated = await _context.Redis.StringSetAsync(cart.UserName, JsonConvert.SerializeObject(cart));

        if (!updated)
        {
            return null;
        }

        return await GetBasket(cart.UserName);
    }

    public async Task<bool> DeleteBasket(string username)
    {
        return await _context.Redis.KeyDeleteAsync(username);
    }
}
