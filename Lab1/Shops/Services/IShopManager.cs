using Shop.Entities;
using Shop.Models;

namespace Shop.Services;

public interface IShopManager
{
    public Shops CreateShop(string shopName, Adress adress);

    public Shops FindShop(Shops shop);
}