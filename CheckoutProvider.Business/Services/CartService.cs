using CheckoutProvider.Business.Interfaces;
using CheckoutProvider.Data.Interfaces;
using CheckoutProvider.Domain.Factories;
using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Business.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly CartFactory _cartFactory;

    public CartService(ICartRepository cartRepository, CartFactory cartFactory)
    {
        _cartRepository = cartRepository;
        _cartFactory = cartFactory;
    }

    public CartServiceResult CreateCart(CartRequest request)
    {
        var checkStockResult = _cartRepository.CheckStock(request);

        if (checkStockResult.Success)
        {
            var product = checkStockResult.ExtractedProductObject;
            var cartEntity = _cartFactory.Create(request, product);

            if (cartEntity != null && product != null)
            {
                var repositoryResult = _cartRepository.Save(cartEntity);

                if (repositoryResult.Success)
                {
                    var cart = _cartFactory.Create(cartEntity);
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

            var cartEntity = _cartFactory.ManageCart(request, productObject, cartListBeforeAddingNewProduct.ExtractedList!);

            if (cartEntity != null && cartListBeforeAddingNewProduct.ExtractedList != null && productObject != null)
            {
                var repositoryResult = _cartRepository.Save(cartEntity);

                if (repositoryResult.Success)
                {
                    var cart = _cartFactory.Create(cartEntity);
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
        var cartEntity = _cartFactory.ManageCartDelete(request, cartListBeforeDelete.ExtractedList!);

        if (cartEntity != null && cartListBeforeDelete.ExtractedList != null)
        {
            var repositoryResult = _cartRepository.Save(cartEntity);

            if (repositoryResult.Success)
            {
                var cart = _cartFactory.Create(cartEntity);
                if (cart != null)
                {
                    return new CartServiceResult { Success = true, Result = cart, Message = "OK", StatusCodes = 200 };
                }
            }
        }
        return new CartServiceResult { Success = false };
    }

}


