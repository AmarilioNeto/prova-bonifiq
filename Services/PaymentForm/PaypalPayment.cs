using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.PaymentForm
{
    public class PaypalPayment: IProvedorPagamento
    {
        public PaypalPayment()
        { 
        }
        public async Task<Order> ProcessPayment(decimal paymentValue, int customerId)
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
