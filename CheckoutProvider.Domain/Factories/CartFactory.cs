using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Domain.Factories;

public class CartFactory
{

    private readonly List<Product> _products = [];

    public CartEntity Create(CartRequest request, Product product)
    {
        _products.Add(new Product
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        });

        return new CartEntity
        {
            CartId = Guid.NewGuid().ToString(),
            UserInfo = request.UserInfo,
            Products = _products
        };
    }
}
