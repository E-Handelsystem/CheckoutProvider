using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Business.Interfaces;

public interface ICartService
{
    CartServiceResult CreateANewCartAndAddProduct(CartRequest request);
    CartServiceResult AddProductToCartList(CartRequest request);
    CartServiceResult DeleteProductFromCartList(CartRequest request);
    CartServiceResult ReduceAmountOfProductInCartList(CartRequest request);
    CartServiceResult IncreaseAmountOfProductInCartList(CartRequest request);
}
