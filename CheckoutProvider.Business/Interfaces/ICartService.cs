using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Business.Interfaces;

public interface ICartService
{
    CartServiceResult CreateCart(CartRequest request);
    CartServiceResult ManageCart(CartRequest request);
}
