using CheckoutProvider.Business.Interfaces;
using CheckoutProvider.Business.Services;
using CheckoutProvider.Data.Interfaces;
using CheckoutProvider.Domain.Factories;
using CheckoutProvider.Domain.Models;
using Moq;

namespace CheckoutProvider.Tests
{
    public class CartService_Tests
    {

        private readonly ICartService _service;
        private readonly Mock<ICartRepository> _mockCartRepository;
        private readonly CartFactory _cartFactory;

        public CartService_Tests()
        {
            _mockCartRepository = new Mock<ICartRepository>();
            _cartFactory = new();
            _service = new CartService(_mockCartRepository.Object, _cartFactory);
        }

        [Fact]
        public void CreateCart_ShouldCreateCart_ReturnSuccess()
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
            var result = _service.CreateCart(request);

            //Assert
            Assert.True(result.Success);
            Assert.Equal(request.ProductName, result.Result.CartProducts.First().Name);
        }

        [Fact]
        public void ManageCart_ShouldAddAProductToAnAlreadyExistingCart_ReturnSuccess()
        {

            //Arrange
            List<Product> _cartList = [];
            _cartList.Add(new Product
            {
                Name = "Leksak",
                Price = "300",
                Id = Guid.NewGuid().ToString()

            });
            var request = new CartRequest { ProductName = "Keps", ProductPrice = "200", ProductAmount = 1, UserInfo = "Mr. Lewis", CartId = Guid.NewGuid().ToString() };
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

            _mockCartRepository.Setup(x => x.CheckStock(It.IsAny<CartRequest>())).Returns(cartRepositoryResult);
            _mockCartRepository.Setup(x => x.Save(It.IsAny<CartEntity>())).Returns(new CartRepositoryResult { Success = true });
            _mockCartRepository.Setup(x => x.AquireCartList(It.IsAny<string>())).Returns(cartRepositoryResult);

            //Act
            var result = _service.ManageCart(request);

            //Assert
            Assert.True(result.Success);
            Assert.True(result.Result.CartProducts.Count() == 2);
            Assert.True(request.CartId == result.Result.CartId);
        }
    }
}