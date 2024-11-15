using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Domain.Factories;

public class CartFactory
{

    private readonly List<Product> _products = [];

    public CartEntity Create(CartRequest request, Product productObject)
    {
        AddProductToList(productObject);

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
            CartProducts = entity.Products
        };
    }

    public CartEntity ManageCart(CartRequest request, Product productObject, List<Product> oldCartList)
    {
        var updatedList = AddProductToList(productObject, oldCartList);

        return new CartEntity
        {
            CartId = request.CartId!,
            UserInfo = request.UserInfo,
            Products = updatedList,
            CategoryName = Guid.NewGuid().ToString()
        };
    }

    public CartEntity ManageCartDelete(CartRequest request, List<Product> oldCartList)
    {
        var updatedList = DeleteProductFromList(request.ProductId!, oldCartList);

        return new CartEntity
        {
            CartId = request.CartId!,
            UserInfo = request.UserInfo,
            Products = updatedList,
            CategoryName = Guid.NewGuid().ToString()
        };
    }

    public void AddProductToList(Product productObject)
    {
        _products.Add(new Product
        {
            Id = productObject.Id,
            Name = productObject.Name,
            Price = productObject.Price
        });

    }

    public List<Product> AddProductToList(Product productObject, List<Product> productList)
    {
        productList.Add(new Product
        {
            Id = productObject.Id,
            Name = productObject.Name,
            Price = productObject.Price
        });

        return productList;
    }

    public List<Product> DeleteProductFromList(string productId, List<Product> productList)
    {
        productList.RemoveAll(x => x.Id.Equals(productId));
        return productList;
    }
}
