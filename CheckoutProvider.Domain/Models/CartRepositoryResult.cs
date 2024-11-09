namespace CheckoutProvider.Domain.Models;

public class CartRepositoryResult : BaseResponseResult
{
    public Product? ExtractedProduct { get; set; }
}
