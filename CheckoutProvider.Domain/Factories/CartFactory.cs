using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Domain.Factories;

public static class CartFactory
{
    public static CartEntity CreateEntity(CartRequest request, List<Product> _productList)
    {
        var entity = new CartEntity
        {
            UserInfo = request.UserInfo,
            Products = _productList,
            CategoryName = Guid.NewGuid().ToString()
        };

        if (request.CartId == null)
            entity.CartId = Guid.NewGuid().ToString();
        else
            entity.CartId = request.CartId;

        return entity;
    }

    public static Cart CreateCart(CartEntity entity)
    {
        return new Cart()
        {
            CartId = entity.CartId,
            UserInfo = entity.UserInfo,
            CartProducts = entity.Products
        };
    }
}
