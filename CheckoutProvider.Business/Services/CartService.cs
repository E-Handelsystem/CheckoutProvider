using CheckoutProvider.Business.Interfaces;
using CheckoutProvider.Data.Interfaces;
using CheckoutProvider.Domain.Factories;
using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Business.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly List<Product> _productList = [];

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public CartServiceResult CreateANewCartAndAddProduct(CartRequest request)
    {
        var checkStockResult = _cartRepository.CheckStock(request);

        if (checkStockResult.Success)
        {
            var product = checkStockResult.ExtractedProductObject;
            AddProductToList(product);
            var cartEntity = CartFactory.CreateEntity(request, _productList);

            if (cartEntity != null && product != null)
            {
                var repositoryResult = _cartRepository.Save(cartEntity);

                if (repositoryResult.Success)
                {
                    var cart = CartFactory.CreateCart(cartEntity);
                    if (cart != null)
                    {
                        return new CartServiceResult { Success = true, Result = cart, Message = "OK", StatusCodes = 200 };
                    }

                }
            }
        }
        return new CartServiceResult { Success = false };
    }

    public CartServiceResult AddProductToCartList(CartRequest request)
    {
        var checkStockResult = _cartRepository.CheckStock(request);

        if (checkStockResult.Success)
        {
            var productObject = checkStockResult.ExtractedProductObject;
            var cartListBeforeAddingNewProduct = _cartRepository.AquireCartList(request.CartId!);
            var cartListAfterAddingNewProduct = AddProductToList(productObject, cartListBeforeAddingNewProduct.ExtractedList!);

            var cartEntity = CartFactory.CreateEntity(request, cartListAfterAddingNewProduct);

            if (cartEntity != null && cartListAfterAddingNewProduct != null && productObject != null)
            {
                var repositoryResult = _cartRepository.Save(cartEntity);

                if (repositoryResult.Success)
                {
                    var cart = CartFactory.CreateCart(cartEntity);
                    if (cart != null)
                    {
                        return new CartServiceResult { Success = true, Result = cart, Message = "OK", StatusCodes = 200 };
                    }

                }
            }
        }
        return new CartServiceResult { Success = false };
    }

    public CartServiceResult DeleteProductFromCartList(CartRequest request)
    {
        var cartListBeforeDelete = _cartRepository.AquireCartList(request.CartId!);
        var cartListAfterDelete = DeleteProductFromList(request.ProductId!, cartListBeforeDelete.ExtractedList!);

        var cartEntity = CartFactory.CreateEntity(request, cartListAfterDelete);

        if (cartEntity != null && cartListAfterDelete != null)
        {
            var repositoryResult = _cartRepository.Save(cartEntity);

            if (repositoryResult.Success)
            {
                var cart = CartFactory.CreateCart(cartEntity);
                if (cart != null)
                {
                    return new CartServiceResult { Success = true, Result = cart, Message = "OK", StatusCodes = 200 };
                }
            }
        }
        return new CartServiceResult { Success = false };
    }

    public CartServiceResult ReduceAmountOfProductInCartList(CartRequest request)
    {
        var cartListBeforeReducedAmount = _cartRepository.AquireCartList(request.CartId!);
        var cartListAfterReducedAmount = DecreaseAmountOfProductInList(request, cartListBeforeReducedAmount.ExtractedList!);

        var cartEntity = CartFactory.CreateEntity(request, cartListAfterReducedAmount);

        if (cartEntity != null && cartListAfterReducedAmount != null)
        {
            var repositoryResult = _cartRepository.Save(cartEntity);

            if (repositoryResult.Success)
            {
                var cart = CartFactory.CreateCart(cartEntity);
                if (cart != null)
                {
                    return new CartServiceResult { Success = true, Result = cart, Message = "OK", StatusCodes = 200 };
                }
            }
        }
        return new CartServiceResult { Success = false };
    }

    public CartServiceResult IncreaseAmountOfProductInCartList(CartRequest request)
    {
        var checkStockResult = _cartRepository.CheckStock(request);

        if (checkStockResult.Success && checkStockResult.AmountOfProductInStock >= request.ProductAmount)
        {
            var cartListBeforeIncreasedAmount = _cartRepository.AquireCartList(request.CartId!);
            var cartListAfterIncreasedAmount = IncreaseAmountOfProductInList(request, cartListBeforeIncreasedAmount.ExtractedList!);

            var cartEntity = CartFactory.CreateEntity(request, cartListAfterIncreasedAmount);

            if (cartEntity != null && cartListAfterIncreasedAmount != null)
            {
                var repositoryResult = _cartRepository.Save(cartEntity);

                if (repositoryResult.Success)
                {
                    var cart = CartFactory.CreateCart(cartEntity);
                    if (cart != null)
                    {
                        return new CartServiceResult { Success = true, Result = cart, Message = "OK", StatusCodes = 200 };
                    }

                }
            }
        }
        return new CartServiceResult { Success = false };
    }

    private List<Product> DecreaseAmountOfProductInList(CartRequest request, List<Product> oldCartList)
    {
        var amountOfProductInList = NumberOfAProductInList(oldCartList, request.ProductId!);
        var updatedCartList = oldCartList;

        while (amountOfProductInList > request.ProductAmount)
        {
            DecreaseAmount(updatedCartList, request.ProductId!);
            amountOfProductInList--;
        }
        return updatedCartList;
    }

    private List<Product> IncreaseAmountOfProductInList(CartRequest request, List<Product> oldCartList)
    {
        var amountOfProductInList = NumberOfAProductInList(oldCartList, request.ProductId!);
        var updatedCartList = oldCartList;

        while (amountOfProductInList < request.ProductAmount)
        {
            IncreaseAmount(updatedCartList, request.ProductId!);
            amountOfProductInList++;
        }

        return updatedCartList;
    }

    private void AddProductToList(Product productObject)
    {
        _productList.Add(new Product
        {
            Id = productObject.Id,
            Name = productObject.Name,
            Price = productObject.Price
        });

    }

    private List<Product> AddProductToList(Product productObject, List<Product> productList)
    {
        productList.Add(new Product
        {
            Id = productObject.Id,
            Name = productObject.Name,
            Price = productObject.Price
        });

        return productList;
    }

    private List<Product> DeleteProductFromList(string productId, List<Product> productList)
    {
        productList.RemoveAll(x => x.Id.Equals(productId));
        return productList;
    }

    private int NumberOfAProductInList(List<Product> products, string productId)
    {
        var amount = products.FindAll(x => x.Id.Equals(productId));

        return amount.Count();
    }

    private List<Product> DecreaseAmount(List<Product> productList, string productId)
    {
        productList.Remove(productList.First(x => x.Id.Equals(productId)));

        return productList;
    }

    private List<Product> IncreaseAmount(List<Product> productList, string productId)
    {
        productList.Add(productList.First(x => x.Id.Equals(productId)));

        return productList;
    }
}




