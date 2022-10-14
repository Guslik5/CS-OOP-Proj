using Shop.Entities;
using Shop.Models;

namespace Shop.Services;

public class ShopManager : IShopManager
{
    private readonly List<Shops> _listShops = new List<Shops>();

    public List<Shops> ListShops => _listShops;

    public Shops CreateShop(string shopName, Adress adress)
    {
        ArgumentNullException.ThrowIfNull(adress, "Adress is null");
        var newshop = new Shops(shopName, adress);
        _listShops.Add(newshop);
        return newshop;
    }

    public Shops FindShop(Shops shop)
    {
        ArgumentNullException.ThrowIfNull(shop, "Shop is null");
        var result = _listShops.FirstOrDefault(s => s.Equals(shop));
        return result;
    }

    public decimal ChepestProductPrice(Product product)
    {
        return _listShops.Where(shop => shop.FindProduct(product) != null)
            .OrderBy(shop => shop.FindProduct(product).PriceProduct)
            .Min(shops => shops.FindProduct(product).PriceProduct);
    }

    public Shops СheapestProductShop(Product product)
    {
        var resultPrice = ChepestProductPrice(product);
        var resultShop = _listShops.Where(shops => shops.FindProduct(product).
            PriceProduct.Equals(resultPrice)).First();
        return resultShop;
    }
}