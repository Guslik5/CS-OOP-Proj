using Shop.Exceptions;
using Shop.Models;
using Shops.Entities;

namespace Shop.Services;

public class ShopManager : IShopManager
{
    private readonly List<Shops.Entities.Shop> _listShops = new List<Shops.Entities.Shop>();

    public Shops.Entities.Shop CreateShop(string shopName, Address address)
    {
        ArgumentNullException.ThrowIfNull(address, "Address is null");
        if (_listShops.Any(s => s.NameShop == shopName & s.AddressShop == address))
        {
            throw new ShopException("Shop has been added");
        }

        var newshop = new Shops.Entities.Shop(shopName, address);
        _listShops.Add(newshop);
        return newshop;
    }

    public Shops.Entities.Shop FindShop(Shops.Entities.Shop shop)
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

    public Shops.Entities.Shop СheapestProductShop(Product product)
    {
        return _listShops.Where(shop => shop.FindProduct(product) != null)
            .MinBy(shops => shops.FindProduct(product).PriceProduct);
    }
}