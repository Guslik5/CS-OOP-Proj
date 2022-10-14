using Shop.Exceptions;

namespace Shop.Models;

public class ElementProductShop : IEquatable<ElementProductShop>
{
    private const int MinCountProduct = 1;
    public ElementProductShop(int countElement, Product product)
    {
        ArgumentNullException.ThrowIfNull(product, "Product is null");
        if (countElement < MinCountProduct)
        {
            throw new CountElementsException("Invalid count elements product");
        }

        (CountElements, Product) = (countElement, product);
    }

    public Product Product { get; }
    internal int CountElements { get; set; }
    public bool Equals(ElementProductShop other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return CountElements == other.CountElements && Product.Equals(other.Product);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ElementProductShop)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CountElements, Product);
    }
}