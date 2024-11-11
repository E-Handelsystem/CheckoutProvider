using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Data.Interfaces;

public interface ICartRepository
{
    public CartRepositoryResult CheckStock(CartRequest request);

    public CartRepositoryResult Save(CartEntity entity);
}
