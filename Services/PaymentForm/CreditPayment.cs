using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.PaymentForm
{
    public class CreditPayment : IProvedorPagamentoCredit
    {
        public CreditPayment()
        {
        }
        public virtual async Task<Order> ProcessPaymentCredit(decimal paymentValue, int customerId)
        {
            // aqui fica a implementação espefica do tipo de pagamento Credito.Aqui tem apena uma generica.
            return await Task.FromResult(new Order()
            {
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.Now,
            });

        }
    }
}
