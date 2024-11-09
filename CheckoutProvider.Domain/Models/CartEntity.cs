namespace CheckoutProvider.Domain.Models;

public class CartEntity
{
    public string CartId { get; set; } = null!;

    public string UserInfo { get; set; } = null!;

    public List<Product> Products { get; set; } = null!;

    public string? CategoryName { get; set; }

}
