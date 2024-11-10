using CheckoutProvider.Business.Interfaces;
using CheckoutProvider.Business.Services;
using CheckoutProvider.Domain.Models;

namespace CheckoutProvider.Tests
{
    public class UnitTest1
    {

        private ICartService _service = new CartService();

        [Fact]
        public void Should()
        {
            //Arrange
            var cartRequest = new CartRequest { ProductName = "Keps", ProductPrice = "200", ProductAmount = 1, UserInfo = "The InZane" };


            //Act
            var result = _service.CreateCart(cartRequest);

            //Assert
        }
    }
}