using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Domain.Factories;

public class CartFactory
{

    private readonly List<Product> _products = [];

    public CartEntity Create(CartRequest request)
    {
        AddProductToList(request);

        return new CartEntity
        {
            CartId = Guid.NewGuid().ToString(),
            UserInfo = request.UserInfo,
            Products = _products,
            CategoryName = Guid.NewGuid().ToString()

        };
    }

    public Cart Create(CartEntity entity)
    {
        return new Cart()
        {
            CartId = entity.CartId,
            UserInfo = entity.UserInfo,
            Products = entity.Products
        };
    }

    public void AddProductToList(CartRequest request)
    {
        _products.Add(new Product
        {
            Id = request.ExtractedProduct.Id,
            Name = request.ExtractedProduct.Name,
            Price = request.ExtractedProduct.Price
        });

    }
}
