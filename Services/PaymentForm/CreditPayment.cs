using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.PaymentForm
{
    public class CreditPayment : IProvedorPagamento
    {
        private readonly TestDbContext _dbContext;
        public CreditPayment()
        {
        }
        public CreditPayment(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Order> ProcessPayment(decimal paymentValue, int customerId)
        {
            var order = new Order
            {
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.Now
            };
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order;

        }
    }
}
