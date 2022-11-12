using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopManager
{
    Shop CreateShop(string shopName, Address address);

    Shop FindShop(Shops.Entities.Shop shop);

    decimal ChepestProductPrice(Product product);

    Shop СheapestProductShop(Product product);
}