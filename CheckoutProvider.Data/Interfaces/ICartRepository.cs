using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Data.Interfaces;

internal interface ICartRepository
{
    public CartRepositoryResult CheckStock(CartRequest request);

    public CartRepositoryResult Save(CartEntity entity);
}
