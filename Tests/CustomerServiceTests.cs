using Microsoft.EntityFrameworkCore;

using Moq;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;
using System.Threading.Tasks;
using System.Linq;

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

        // parei aqui verificando se não tem mais possiveis casos de teste e tenedo melhorar e limpar o codigo  
        [Fact]
        public async Task ValidCustomerAndValue()
        {
  
            using var context = new TestDbContext(_dbContextOptions);
            var service = new CustomerService(context);
            var customerId = 1;
            var purchaseValue = 50;

            var result = await service.CanPurchase(customerId, purchaseValue);
            Assert.True(result);

        }
        [Fact]
        public async Task NonRegisteredCustomer()
        {
    
            using var context = new TestDbContext(_dbContextOptions);
            var service = new CustomerService(context);
            var customerId = 999; 
            var purchaseValue = 50;

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await service.CanPurchase(customerId, purchaseValue);
            });
        }
        [Fact]
        public async Task MultiplePurchasesInOneMonth()
        {

            using var context = new TestDbContext(_dbContextOptions);
            var service = new CustomerService(context);
            var customerId = 1;
            var lastMonthOrder = new Order { CustomerId = customerId, OrderDate = DateTime.UtcNow.AddMonths(-1) };
            context.Orders.Add(lastMonthOrder);
            await context.SaveChangesAsync();

            var purchaseValue = 50;

            var result = await service.CanPurchase(customerId, purchaseValue);
    
            Assert.False(result);
        }

        [Fact]
        public async Task FirstTimeCustomerExceedsMaximum()
        {
          
            using var context = new TestDbContext(_dbContextOptions);
            var service = new CustomerService(context);
            var customerId = 2;

            var purchaseValue = 150;

            var result = await service.CanPurchase(customerId, purchaseValue);

            Assert.False(result);
        }
        [Fact]
        public async Task FirstTimeCustomerWithinMaximum()
        {
 
            using var context = new TestDbContext(_dbContextOptions);
            var service = new CustomerService(context);
            var customerId = 3;

            var purchaseValue = 50;

            var result = await service.CanPurchase(customerId, purchaseValue);

            Assert.True(result);
        }
        [Fact]
        public async Task CustomerWithPreviousPurchaseWithinMaximum()
        {

            using var context = new TestDbContext(_dbContextOptions);
            var service = new CustomerService(context);
            var customerId = 4;

            var previousOrder = new Order { CustomerId = customerId, OrderDate = DateTime.UtcNow.AddMonths(-2) };
            context.Orders.Add(previousOrder);
            await context.SaveChangesAsync();

            var purchaseValue = 80;
            var result = await service.CanPurchase(customerId, purchaseValue);

            Assert.True(result);
        }
    }
}
