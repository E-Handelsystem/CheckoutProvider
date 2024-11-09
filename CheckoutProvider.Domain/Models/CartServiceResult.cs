namespace CheckoutProvider.Domain.Models;

public class CartServiceResult : BaseResponseResult
{
    public Cart? Result { get; set; }
}
