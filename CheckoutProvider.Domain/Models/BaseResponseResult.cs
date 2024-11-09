namespace CheckoutProvider.Domain.Models;

public abstract class BaseResponseResult
{
    public bool Success { get; set; }

    public int StatusCodes { get; set; }

    public string? Message { get; set; }
}