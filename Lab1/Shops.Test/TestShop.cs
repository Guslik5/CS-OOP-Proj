using Shop.Exceptions;
using Shop.Models;
using Shop.Services;
using Shops.Entities;
using Xunit;
namespace Shop.Test;

public class ShopTest
{
    private ShopManager _shopManager = new ShopManager();
    private Product _product1 = new Product("sigi", 130);
    private Product _product2 = new Product("pivo", 100);
    private Product _product3 = new Product("sigi", 130);
    private Person _person1 = new Person("kerill", 2000);

    [Fact]
    public void AddProduct()
    {
        var address = new Address("Glinki 11");
        var shop = _shopManager.CreateShop("Magazin Kirilla", address);
        shop.AddProducts(_product1, 5);
        shop.AddProducts(_product2, 5);
        shop.AddProducts(_product3, 3);
        shop.AddProducts(_product3, 3);
        Assert.Equal(2, shop.ProductsShop.Count());
    }

    [Fact]
    public void ChangePrice()
    {
        var address = new Address("Glinki 11");
        var shop = _shopManager.CreateShop("Magazin Kirilla", address);
        shop.AddProducts(_product1, 5);
        shop.ChangePrice(_product1, 500);
        Assert.Equal(130, _product1.PriceProduct);
    }

    [Fact]
    public void BuyProduct()
    {
        var address = new Address("Glinki 11");
        var shop = _shopManager.CreateShop("Magazin Kirilla", address);
        shop.AddProducts(_product1, 2);
        shop.BuyProduct(_person1, 2, _product1);
        Assert.Empty(shop.ProductsShop);
    }

    [Fact]
    public void BuyProductsIsFaild()
    {
        var address = new Address("Glinki 11");
        var shop = _shopManager.CreateShop("Magazin Kirilla", address);
        shop.AddProducts(_product1, 7);
        shop.AddProducts(_product2, 5);
        shop.AddProducts(_product3, 3);
        List<ProductForBuy> shopingList = new List<ProductForBuy>();
        var productBuy1 = new ProductForBuy(20, _product1);
        var productBuy2 = new ProductForBuy(2, _product2);
        shopingList.Add(productBuy1);
        shopingList.Add(productBuy2);
        Assert.Throws<BuyProductsException>(() => shop.BuyProducts(_person1, shopingList));
    }

    [Fact]
    public void FindProduct()
    {
        var address = new Address("Glinki 11");
        var shop = _shopManager.CreateShop("Magazin Kirilla", address);
        shop.AddProducts(_product1, 7);
        shop.AddProducts(_product2, 5);
        shop.AddProducts(_product3, 3);
        var findProduct = shop.FindProduct(_product1);
        Assert.NotNull(findProduct);
    }

    [Fact]
    public void FindShop()
    {
        var address = new Address("Glinki 11");
        var shop1 = _shopManager.CreateShop("Magazin one", address);
        var shop2 = _shopManager.CreateShop("Magazin two", address);
        var shop3 = _shopManager.CreateShop("Magazin three", address);
        var resultshop = _shopManager.FindShop(shop1);
        Assert.NotNull(resultshop);
    }

    [Fact]
    public void CheapestProduct()
    {
        var address1 = new Address("Glinki 1");
        var address2 = new Address("Glinki 2");
        var address3 = new Address("Glinki 3");
        var shop1 = _shopManager.CreateShop("Magazin one", address1);
        var shop2 = _shopManager.CreateShop("Magazin two", address2);
        var shop3 = _shopManager.CreateShop("Magazin three", address3);
        shop1.AddProducts(_product1, 5);
        shop2.AddProducts(_product2, 5);
        shop1.ChangePrice(_product1, 150);
        shop2.ChangePrice(_product2, 100);
        var сheapestProductShop = _shopManager.СheapestProductShop(_product2);
        var сheapestProductPrice = _shopManager.ChepestProductPrice(_product2);
        Assert.Equal(shop2, сheapestProductShop);
        Assert.Equal(100, сheapestProductPrice);
    }
}
