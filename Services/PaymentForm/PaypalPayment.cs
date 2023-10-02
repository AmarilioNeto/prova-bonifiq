using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.PaymentForm
{
    public class PaypalPayment: IProvedorPagamentoPaypal
    {
        public PaypalPayment()
        { 
        }
        public async Task<Order> ProcessPaymentPayPal(decimal paymentValue, int customerId)
        {
            // aqui fica a implementação espefica do tipo de pagamento paypal.Aqui tem apena uma generica.
            return await Task.FromResult(new Order()
            {
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.Now,
            });
        }
    }
}
