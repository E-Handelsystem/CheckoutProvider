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
            _service = new CartService(_mockCartRepository.Object, _cartFactory);
            _mockCartRepository = new Mock<ICartRepository>();
            _cartFactory = new CartFactory();
        }

        [Fact]
        public void CreateCart_ShouldCreateCart_ReturnSuccess()
        {
            //Arrange
            var request = new CartRequest { ProductName = "Keps", ProductPrice = "200", ProductAmount = 1, UserInfo = "The InZane" };
            var cartRepositoryResult = new CartRepositoryResult
            {
                Success = true,
                ExtractedProduct = new Product
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
        }
    }
}