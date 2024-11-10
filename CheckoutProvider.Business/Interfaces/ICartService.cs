using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Business.Interfaces;

public interface ICartService
{
    CartRepositoryResult CheckStock(CartRequest request);
    CartServiceResult CreateCart(CartRequest request);
}
