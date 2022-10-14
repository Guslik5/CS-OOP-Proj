using Shop.Entities;
using Shop.Models;
using Shop.Services;
using Xunit;
namespace Shop.Test;

public class ShopTest
{
    private ShopManager _shopManager = new ShopManager();
    [Fact]
    public void AddProduct()
    {
        var product1 = new Product("sigi", 130);
        var product2 = new Product("pivo", 130);
        var product3 = new Product("sigi", 130);
        var adress = new Adress("Glinki 11");
        var shop = _shopManager.CreateShop("Magazin Kirilla", adress);
        shop.AddProducts(product1, 5);
        shop.AddProducts(product2, 5);
        shop.AddProducts(product3, 3);
        shop.AddProducts(product3, 3);
        Assert.True(shop.ProductsShop.Count() == 2);
    }

    [Fact]
    public void ChangePrice()
    {
        var product1 = new Product("sigi", 130);
        var adress = new Adress("Glinki 11");
        var shop = _shopManager.CreateShop("Magazin Kirilla", adress);
        shop.AddProducts(product1, 5);
        shop.ChangePrice(product1, 500);
        Assert.True(product1.PriceProduct.Equals(130));
        Assert.True(shop.ProductsShop[0].Product.PriceProduct.Equals(500));
    }

    [Fact]
    public void BuyProduct()
    {
        Person person1 = new Person("kerill", 260);
        var product1 = new Product("sigi", 130);
        var adress = new Adress("Glinki 11");
        var shop = _shopManager.CreateShop("Magazin Kirilla", adress);
        shop.AddProducts(product1, 2);
        shop.BuyProduct(person1, 2, product1);
        Assert.True(!shop.ProductsShop.Any());
    }

    [Fact]
    public void BuyProducts()
    {
        Person person1 = new Person("kerill", 2000);
        var product1 = new Product("sigi", 100);
        var product2 = new Product("pivo", 200);
        var product3 = new Product("sigi", 100);
        var adress = new Adress("Glinki 11");
        var shop = _shopManager.CreateShop("Magazin Kirilla", adress);
        shop.AddProducts(product1, 7);
        shop.AddProducts(product2, 5);
        shop.AddProducts(product3, 3);
        List<ProductForBuy> shopingList = new List<ProductForBuy>();
        shopingList.Add(new ProductForBuy(8, product1));
        shopingList.Add(new ProductForBuy(5, product2));
        shop.BuyProducts(person1, shopingList);
    }

    [Fact]
    public void FindProduct()
    {
        Person person1 = new Person("kerill", 2000);
        var product1 = new Product("sigi", 100);
        var product2 = new Product("pivo", 200);
        var product3 = new Product("sigi", 100);
        var adress = new Adress("Glinki 11");
        var shop = _shopManager.CreateShop("Magazin Kirilla", adress);
        shop.AddProducts(product1, 7);
        shop.AddProducts(product2, 5);
        shop.AddProducts(product3, 3);
        var findProduct = shop.FindProduct(product1);
        Assert.False(findProduct is null);
    }

    [Fact]
    public void FindShop()
    {
        var adress = new Adress("Glinki 11");
        var shop1 = _shopManager.CreateShop("Magazin one", adress);
        var shop2 = _shopManager.CreateShop("Magazin two", adress);
        var shop3 = _shopManager.CreateShop("Magazin three", adress);
        var resultshop = _shopManager.FindShop(shop3);
        Assert.False(resultshop is null);
    }

    [Fact]
    public void CheapestProduct()
    {
        var adress1 = new Adress("Glinki 1");
        var adress2 = new Adress("Glinki 2");
        var adress3 = new Adress("Glinki 3");
        var shop1 = _shopManager.CreateShop("Magazin one", adress1);
        var shop2 = _shopManager.CreateShop("Magazin two", adress2);
        var shop3 = _shopManager.CreateShop("Magazin three", adress3);
        var product = new Product("Pivo", 100);
        shop1.AddProducts(product, 5);
        shop1.ChangePrice(product, 150);
        shop2.AddProducts(product, 5);
        shop2.ChangePrice(product, 100);
        shop3.AddProducts(product, 5);
        shop3.ChangePrice(product, 10);
        var сheapestProductShop = _shopManager.СheapestProductShop(product);
        var сheapestProductPrice = _shopManager.ChepestProductPrice(product);
        Assert.True(сheapestProductShop.Equals(shop3));
        Assert.True(сheapestProductPrice == 10);
    }
}
