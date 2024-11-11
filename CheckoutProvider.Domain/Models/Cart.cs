namespace CheckoutProvider.Domain.Models;

public class Cart
{
    public string CartId { get; set; } = null!;

    public string UserInfo { get; set; } = null!;

    public List<Product> CartProducts { get; set; } = null!;
}