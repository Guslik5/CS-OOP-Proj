using System.Net.Sockets;
using Microsoft.VisualBasic;
using Shop.Exceptions;
using Shop.Models;

namespace Shop.Entities;

public class Shops : IEquatable<Shops>
{
    private readonly List<ElementProductShop> _productsShop = new List<ElementProductShop>();
    internal Shops(string nameShop, Adress adressShop)
    {
        NameShop = nameShop;
        AdressShop = adressShop;
        GuidShop = Guid.NewGuid();
    }

    public List<ElementProductShop> ProductsShop => _productsShop;

    public string NameShop { get; }
    public Adress AdressShop { get; }
    public Guid GuidShop { get; }

    public bool Equals(Shops other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _productsShop.Equals(other._productsShop) && NameShop == other.NameShop && AdressShop.Equals(other.AdressShop) && GuidShop.Equals(other.GuidShop);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Shops)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_productsShop, NameShop, AdressShop, GuidShop);
    }

    public void AddProducts(Product product, int quantity)
    {
        if (!_productsShop.Any())
        {
            _productsShop.Add(new ElementProductShop(quantity, product));
        }
        else
        {
            var result = _productsShop.Where(p => p.Product.Equals(product));
            if (!result.Any())
            {
                _productsShop.Add(new ElementProductShop(quantity, product));
            }
            else
            {
                foreach (ElementProductShop elementProductShop in _productsShop)
                {
                    if (elementProductShop.Product.Equals(product))
                    {
                        var oldQuantity = elementProductShop.CountElements;
                        _productsShop.Remove(elementProductShop);
                        _productsShop.Add(new ElementProductShop(quantity + oldQuantity, product));
                        break;
                    }
                }
            }
        }
    }

    public void ChangePrice(Product product, decimal newPrice)
    {
        ArgumentNullException.ThrowIfNull(product, "Product for change price is null");
        var productChange = _productsShop.FirstOrDefault(s => s.Product.Equals(product));
        if (productChange is null)
        {
            throw new ProductChangeException("Product not found");
        }

        var copyProduct = new ElementProductShop(productChange.CountElements, new Product(productChange.Product.NameProduct, newPrice));
        _productsShop.Remove(productChange);
        _productsShop.Add(copyProduct);
    }

    public void BuyProduct(Person person, int countProducts, Product product)
    {
        ArgumentNullException.ThrowIfNull(person, "Person is null");
        ArgumentNullException.ThrowIfNull(product, "Product for buy is null");
        var result = _productsShop.FirstOrDefault(s => s.Product.Equals(product));
        if (result is null)
        {
            throw new ProductBuyException("Product for buy not found");
        }

        if (person.Money < product.PriceProduct * countProducts)
        {
            throw new BuyProductException("Person does not have money to buy all products");
        }

        if (result.CountElements < countProducts)
        {
            throw new BuyProductException("Exceeded the number of products");
        }
        else if (result.CountElements == countProducts)
        {
            _productsShop.Remove(result);
            person.Money -= product.PriceProduct * countProducts;
        }
        else
        {
            result.CountElements -= countProducts;
            person.Money -= product.PriceProduct * countProducts;
        }
    }

    public void BuyProducts(Person person, List<ProductForBuy> productsBuy)
    {
        ArgumentNullException.ThrowIfNull(person, "Person is null");
        ArgumentNullException.ThrowIfNull(productsBuy, "Products for buy is null");
        foreach (var product in productsBuy)
        {
            this.BuyProduct(person, product.CountElements, product.Product);
        }
    }

    public Product FindProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product, "Product for find is null");
        var findProduct = _productsShop.FirstOrDefault(a => a.Product.NameProduct.Equals(product.NameProduct));
        return findProduct.Product;
    }
}