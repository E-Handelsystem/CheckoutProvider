namespace CheckoutProvider.Domain.Models;

public class CartRequest
{
    public string? CartId { get; set; }

    public string UserInfo { get; set; } = null!;

    public string PickedProduct { get; set; } = null!;

    public int AmountOfProducts { get; set; }
}
