using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Business.Interfaces;

public interface ICartService
{
    CartServiceResult CheckStock(CartRequest request);
    CartServiceResult CreateCart(CartRequest request);
}
