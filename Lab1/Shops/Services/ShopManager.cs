using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopManager : IShopManager
{
    private readonly List<Shop> _listShops = new List<Shops.Entities.Shop>();

    public Shop CreateShop(string shopName, Address address)
    {
        ArgumentNullException.ThrowIfNull(address, "Address is null");
        if (_listShops.Any(s => s.NameShop == shopName & s.AddressShop == address))
        {
            throw new ShopException("Shop has been added");
        }

        var newshop = new Shop(shopName, address);
        _listShops.Add(newshop);
        return newshop;
    }

    public Shop FindShop(Shops.Entities.Shop shop)
    {
        ArgumentNullException.ThrowIfNull(shop, "Shop is null");
        var result = _listShops.FirstOrDefault(s => s.Equals(shop));
        return result;
    }

    public decimal ChepestProductPrice(Product product)
    {
        return _listShops.Where(shop => shop.FindProduct(product) != null)
            .MinBy(shops => shops.FindProduct(product).PriceProduct)
            .FindProduct(product).PriceProduct;
    }

    public Shop СheapestProductShop(Product product)
    {
        return _listShops.Where(shop => shop.FindProduct(product) != null)
            .MinBy(shops => shops.FindProduct(product).PriceProduct);
    }
}