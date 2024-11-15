namespace CheckoutProvider.Domain.Models;

public class CartRequest
{
    public string? CartId { get; set; }

    public string UserInfo { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string ProductPrice { get; set; } = null!;

    public string? ProductId { get; set; }

    public int ProductAmount { get; set; }
}
