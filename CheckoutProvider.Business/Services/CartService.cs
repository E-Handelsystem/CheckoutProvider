using CheckoutProvider.Business.Interfaces;
using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Business.Services;

public class CartService : ICartService
{
    public CartServiceResult CreateCart(CartRequest request)
    {
        var stockResult = CheckStock(request);

        if (stockResult.Success == true)
        {
            request.ExtractedProduct = stockResult.ExtractedProduct!;
        }
    }

    public CartRepositoryResult CheckStock(CartRequest request)
    {

        //Returnar en CartRepositoryResult då jag inte kommer bygga funktionaliteten i CartRepository
        return new CartRepositoryResult
        {
            Success = true,
            StatusCodes = 200,
            Message = "OK",
            ExtractedProduct = new Product
            {
                Name = request.ProductName,
                Price = request.ProductPrice,
                Id = Guid.NewGuid().ToString()
            }
        };
    }



}
