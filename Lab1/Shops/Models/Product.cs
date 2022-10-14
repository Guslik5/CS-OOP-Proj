using Shop.Exceptions;

namespace Shop.Models;

public class Product : IEquatable<Product>
{
    private const decimal MinPriceProduct = 0;
    public Product(string nameProduct, decimal priceProduct)
    {
        if (priceProduct < MinPriceProduct)
        {
            throw new PriceProductException("invalid price Product");
        }

        NameProduct = nameProduct;
        PriceProduct = priceProduct;
        GuidProduct = Guid.NewGuid();
    }

    public string NameProduct { get; }

    public Guid GuidProduct { get; }
    public decimal PriceProduct { get; private set; }

    public void ChangePriceProduct(decimal newPrice)
    {
        if (newPrice < MinPriceProduct)
        {
            throw new PriceProductException("Invalid new price product");
        }

        PriceProduct = newPrice;
    }

    public bool Equals(Product other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return NameProduct == other.NameProduct && PriceProduct.Equals(other.PriceProduct);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Product)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(NameProduct, PriceProduct);
    }
}