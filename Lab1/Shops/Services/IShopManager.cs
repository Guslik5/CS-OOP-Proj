using Shop.Entities;
using Shop.Models;

namespace Shop.Services;

public interface IShopManager
{
    public Shops CreateShop(string shopName, Address adress);

    public Shops FindShop(Shops shop);

    public decimal ChepestProductPrice(Product product);

    public Shops СheapestProductShop(Product product);
}