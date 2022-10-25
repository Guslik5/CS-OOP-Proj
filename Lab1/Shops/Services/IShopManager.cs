using Shop.Models;

namespace Shop.Services;

public interface IShopManager
{
    Shops.Entities.Shop CreateShop(string shopName, Address address);

    Shops.Entities.Shop FindShop(Shops.Entities.Shop shop);

    decimal ChepestProductPrice(Product product);

    Shops.Entities.Shop СheapestProductShop(Product product);
}