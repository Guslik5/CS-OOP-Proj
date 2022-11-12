using Shops.Exceptions;

namespace Shops.Models;

public class ProductForBuy
{
    private const int MinCountProduct = 1;

    public ProductForBuy(int countElement, Product product)
    {
        ArgumentNullException.ThrowIfNull(product, "Product is null");
        if (countElement < MinCountProduct)
        {
            throw new ProductBuyException("invalid count of products");
        }

        (CountElements, Product) = (countElement, product);
    }

    public Product Product { get; }
    internal int CountElements { get; set; }
}