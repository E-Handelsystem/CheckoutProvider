namespace CheckoutProvider.Domain.Models;

public class CartRepositoryResult : BaseResponseResult
{
    public Product ExtractedProductObject { get; set; } = null!;

    public List<Product>? ExtractedList { get; set; }

    public int AmountOfProductInStock { get; set; }
}
