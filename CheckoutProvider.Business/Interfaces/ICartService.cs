using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Business.Interfaces;

public interface ICartService
{
    CartServiceResult CreateCart(CartRequest request);
    CartServiceResult AddProductToCartList(CartRequest request);
    CartServiceResult DeleteProductFromCartList(CartRequest request);
    CartServiceResult ReduceAmountOfProduct(CartRequest request);
    CartServiceResult IncreaseAmountOfProduct(CartRequest request);
}
