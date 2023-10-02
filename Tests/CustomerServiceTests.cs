using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;



namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        private readonly DbContextOptions<TestDbContext> _dbContextOptions;


        public CustomerServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Teste;Trusted_Connection=True;")
                .Options;
        }
        private (TestDbContext context, CustomerService service) CreateContextAndService()
        {
            var context = new TestDbContext(_dbContextOptions);
            var service = new CustomerService(context);
            return (context, service);
        }

        [Fact]
        public async Task ClienteValorValidos_ReturnsTrue()
        {
            // Arrange
            var (context, service) = CreateContextAndService();
            var customerId = 7;
            var purchaseValue = 50;
            // Act
            var result = await service.CanPurchase(customerId, purchaseValue);
            // Assert
            Assert.True(result);
            context.Dispose();
        }
        [Fact]
        public async Task ClienteNaoRegistrado_ThrowsException()
        {
            // Arrange
            var (context, service) = CreateContextAndService();
            var customerId = 999;
            var purchaseValue = 50;
            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await service.CanPurchase(customerId, purchaseValue);
            });        
            context.Dispose();
        }
        [Fact]
        public async Task VariasComprasNoMes_ReturnsFalse()
        {
            // Arrange
            var (context, service) = CreateContextAndService();
            var customerId = 11;
            var lastMonthOrder1 = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now
            };
            var lastMonthOrder2 = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now
            };
            context.Orders.AddRange(lastMonthOrder1, lastMonthOrder2);
            var test = await context.SaveChangesAsync();
            var purchaseValue = 50;

            //Act
            var result = await service.CanPurchase(customerId, purchaseValue);

            //Assert
            Assert.False(result);
            context.Dispose();
        }


        [Fact]
        public async Task ClientePelaPrimeiraVezExcedeMaximo_ReturnsFalse()
        {

            // Arrange
            var (context, service) = CreateContextAndService();
            var customerId = 2;
            var purchaseValue = 150;

            //Act
            var result = await service.CanPurchase(customerId, purchaseValue);

            //Assert
            Assert.False(result);
            context.Dispose();
        }
        [Fact]
        public async Task ClienteCompraPelaPrimeiraVezDentroMaximo_ReturnsTrue()
        {
            // Arrange
            var (context, service) = CreateContextAndService();
            var customerId = 3;
            var purchaseValue = 50;

            //Act
            var result = await service.CanPurchase(customerId, purchaseValue);

            //Assert
            Assert.True(result);
            context.Dispose();
        }
        [Fact]
        public async Task ClienteComCompraAnteriorDentroMáximo_ReturnsTrue()
        {
            // Arrange
            var (context, service) = CreateContextAndService();
            var customerId = 15;
            var purchaseValue = 100;

            var previousOrder = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow.AddMonths(-2)
            };
            context.Orders.Add(previousOrder);
            await context.SaveChangesAsync();

            //Act
            var result = await service.CanPurchase(customerId, purchaseValue);

            //Assert
            Assert.True(result);
            context.Dispose();
        }
    }
}
