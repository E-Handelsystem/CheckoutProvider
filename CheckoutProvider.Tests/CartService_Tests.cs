using CheckoutProvider.Business.Interfaces;
using CheckoutProvider.Business.Services;
using CheckoutProvider.Data.Interfaces;
using CheckoutProvider.Domain.Models;
using Moq;

namespace CheckoutProvider.Tests;

public class CartService_Tests
{

    private readonly ICartService _service;
    private readonly Mock<ICartRepository> _mockCartRepository;

    public CartService_Tests()
    {
        _mockCartRepository = new Mock<ICartRepository>();
        _service = new CartService(_mockCartRepository.Object);
    }

    [Fact]
    public void CreateCart_ShouldCreateANewCartAndAddAProductToIt_ReturnSuccess()
    {
        //Arrange
        var request = new CartRequest { ProductName = "Keps", ProductPrice = "200", ProductAmount = 1, UserInfo = "Mr. Lewis" };
        var cartRepositoryResult = new CartRepositoryResult
        {
            Success = true,
            ExtractedProductObject = new Product
            {
                Name = request.ProductName,
                Price = request.ProductPrice,
                Id = Guid.NewGuid().ToString()
            }
        };
        _mockCartRepository.Setup(x => x.CheckStock(It.IsAny<CartRequest>())).Returns(cartRepositoryResult);
        _mockCartRepository.Setup(x => x.Save(It.IsAny<CartEntity>())).Returns(new CartRepositoryResult { Success = true });

        //Act
        var result = _service.CreateANewCartAndAddProduct(request);

        //Assert
        Assert.True(result.Success);
        Assert.Equal(request.ProductName, result.Result.CartProducts.First().Name);
    }

    [Fact]
    public void ManageCart_ShouldAddAnotherProductToAnAlreadyExistingCart_ReturnSuccess()
    {
        //Arrange

        var request = new CartRequest { ProductName = "Keps", ProductPrice = "200", ProductAmount = 1, UserInfo = "Mr. Lewis", CartId = Guid.NewGuid().ToString() };

        List<Product> _cartList = [];
        _cartList.Add(new Product
        {
            Name = "Leksak",
            Price = "300",
            Id = Guid.NewGuid().ToString()

        });

        var cartRepositoryResult = new CartRepositoryResult
        {
            Success = true,
            ExtractedProductObject = new Product
            {
                Name = request.ProductName,
                Price = request.ProductPrice,
                Id = Guid.NewGuid().ToString()
            },
            ExtractedList = _cartList
        };

        //Returnar sammma object (cartResultResultat) tv� g�nger d� jag �r lat och d� det inte �r ett riktigt project, d�r jag annars inte hade gjort p� detta s�tt.
        _mockCartRepository.Setup(x => x.CheckStock(It.IsAny<CartRequest>())).Returns(cartRepositoryResult);
        _mockCartRepository.Setup(x => x.Save(It.IsAny<CartEntity>())).Returns(new CartRepositoryResult { Success = true });
        _mockCartRepository.Setup(x => x.AquireCartList(It.IsAny<string>())).Returns(cartRepositoryResult);

        //Act
        var result = _service.AddProductToCartList(request);

        //Assert
        Assert.True(result.Success);
        Assert.True(result.Result.CartProducts.Count() == 2);
        Assert.True(request.CartId == result.Result.CartId);
    }

    [Fact]

    public void DeleteProductFromCart_ShouldDeleteProductFromAnExistingCart_ReturnSuccess()
    {
        //Arrange
        List<Product> _cartList = [];
        _cartList.Add(new Product
        {
            Name = "Leksak",
            Price = "300",
            Id = Guid.NewGuid().ToString()

        });

        var request = new CartRequest
        {
            ProductName = "Leksak",
            ProductPrice = "300",
            ProductAmount = 1,
            UserInfo = "Mr. Lewis",
            CartId = Guid.NewGuid().ToString(),
            ProductId = _cartList.First().Id
        };

        var cartRepositoryResult = new CartRepositoryResult { ExtractedList = _cartList };
        _mockCartRepository.Setup(x => x.Save(It.IsAny<CartEntity>())).Returns(new CartRepositoryResult { Success = true });
        _mockCartRepository.Setup(x => x.AquireCartList(It.IsAny<string>())).Returns(cartRepositoryResult);

        //Act
        var result = _service.DeleteProductFromCartList(request);

        //Assert
        Assert.True(result.Success);
        Assert.True(result.Result.CartProducts.Count() == 0);
    }

    [Fact]
    public void ReduceAmountOfProduct_ShouldReduceTheAmountOfAProductInTheCart_ReturnSuccess()
    {
        //Arrange
        var request = new CartRequest
        {
            ProductName = "Leksak",
            ProductPrice = "300",
            ProductAmount = 1,
            UserInfo = "Mr. Lewis",
            CartId = Guid.NewGuid().ToString(),
            ProductId = Guid.NewGuid().ToString()
        };

        List<Product> _cartList = [];
        _cartList.Add(new Product { Name = "Leksak", Price = "300", Id = request.ProductId });
        _cartList.Add(new Product { Name = "Leksak", Price = "300", Id = request.ProductId });

        var cartRepositoryResult = new CartRepositoryResult { ExtractedList = _cartList };
        _mockCartRepository.Setup(x => x.Save(It.IsAny<CartEntity>())).Returns(new CartRepositoryResult { Success = true });
        _mockCartRepository.Setup(x => x.AquireCartList(It.IsAny<string>())).Returns(cartRepositoryResult);

        //Act
        var result = _service.ReduceAmountOfProductInCartList(request);

        //Assert
        Assert.True(result.Success);
        Assert.True(result.Result.CartProducts.Count() == 1);
    }

    [Fact]

    public void IncreaseAmountOfProduct_ShouldIncreaseTheAmountOfAProductInTheCart_ReturnSuccess()
    {
        var request = new CartRequest
        {
            ProductName = "Leksak",
            ProductPrice = "300",
            ProductAmount = 2,
            UserInfo = "Mr. Lewis",
            CartId = Guid.NewGuid().ToString(),
            ProductId = Guid.NewGuid().ToString()
        };

        List<Product> _cartList = [];
        _cartList.Add(new Product { Name = "Leksak", Price = "300", Id = request.ProductId });

        var cartRepositoryResult = new CartRepositoryResult { Success = true, ExtractedList = _cartList, AmountOfProductInStock = 2, };
        _mockCartRepository.Setup(x => x.CheckStock(It.IsAny<CartRequest>())).Returns(cartRepositoryResult);
        _mockCartRepository.Setup(x => x.Save(It.IsAny<CartEntity>())).Returns(new CartRepositoryResult { Success = true });
        _mockCartRepository.Setup(x => x.AquireCartList(It.IsAny<string>())).Returns(cartRepositoryResult);

        //Act
        var result = _service.IncreaseAmountOfProductInCartList(request);

        //Assert
        Assert.True(result.Success);
        Assert.True(result.Result.CartProducts.Count() == 2);

    }
}