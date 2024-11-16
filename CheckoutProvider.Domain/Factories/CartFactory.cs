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

    public CartEntity ManageCartAddNewProduct(CartRequest request, Product productObject, List<Product> oldCartList)
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

    public CartEntity ManageCartDeleteProduct(CartRequest request, List<Product> oldCartList)
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

    public CartEntity ManageCartDecreaseAmount(CartRequest request, List<Product> oldCartList)
    {
        var amountOfProductInList = NumberOfAProductInList(oldCartList, request.ProductId!);
        var updatedCartList = oldCartList;

        while (amountOfProductInList > request.ProductAmount)
        {
            DecreaseAmount(updatedCartList, request.ProductId!);
            amountOfProductInList--;
        }

        return new CartEntity
        {
            CartId = request.CartId!,
            UserInfo = request.UserInfo,
            Products = updatedCartList,
            CategoryName = Guid.NewGuid().ToString()
        };
    }

    public CartEntity ManageCartIncreaseAmount(CartRequest request, List<Product> oldCartList)
    {
        var amountOfProductInList = NumberOfAProductInList(oldCartList, request.ProductId!);
        var updatedCartList = oldCartList;

        while (amountOfProductInList < request.ProductAmount)
        {
            IncreaseAmount(updatedCartList, request.ProductId!);
            amountOfProductInList++;
        }

        return new CartEntity
        {
            CartId = request.CartId!,
            UserInfo = request.UserInfo,
            Products = updatedCartList,
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

    public int NumberOfAProductInList(List<Product> products, string productId)
    {
        var amount = products.FindAll(x => x.Id.Equals(productId));

        return amount.Count();
    }

    public List<Product> DecreaseAmount(List<Product> productList, string productId)
    {
        productList.Remove(productList.First(x => x.Id.Equals(productId)));

        return productList;
    }

    public List<Product> IncreaseAmount(List<Product> productList, string productId)
    {
        productList.Add(productList.First(x => x.Id.Equals(productId)));

        return productList;
    }
}
