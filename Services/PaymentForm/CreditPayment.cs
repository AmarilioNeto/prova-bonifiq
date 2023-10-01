using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.PaymentForm
{
    public class CreditPayment : IProvedorPagamento
    {
        public CreditPayment()
        {
        }
        public virtual async Task<Order> ProcessPayment(decimal paymentValue, int customerId)
        {
            return await Task.FromResult(new Order()
            {
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.Now,
            });

        }
    }
}
