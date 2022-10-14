namespace Shops.Models;

public class ShopingList
{
    public ShopingList(int countElement, Product product)
    {
        ArgumentNullException.ThrowIfNull(product, "Product is null");
        (CountElements, Product) = (countElement, product);
    }

    public Product Product { get; }
    internal int CountElements { get; set; }
}