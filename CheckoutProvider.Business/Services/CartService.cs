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
        var stockResult = _cartRepository.CheckStock(request);

        if (stockResult.Success)
        {
            var product = stockResult.ExtractedProductObject;
            var cartEntity = _cartFactory.Create(request, product);

            if (cartEntity != null)
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

    public CartServiceResult ManageCart(CartRequest request)
    {
        var stockResult = _cartRepository.CheckStock(request);

        if (stockResult.Success)
        {
            var productObject = stockResult.ExtractedProductObject;
            var productList = _cartRepository.AquireCartList(request.CartId!);

            var cartEntity = _cartFactory.ManageCart(request, productObject, productList.ExtractedList!);

            if (cartEntity != null)
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
}
