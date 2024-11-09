using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Domain.Factories;

public class CartFactory
{

    private List<Product> products = new();

    public CartEntity Create(CartRequest request)
    {
        return new CartEntity
        {
            CartId = Guid.NewGuid().ToString(),

        };
    }
}
