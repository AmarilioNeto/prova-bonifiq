using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.PaymentForm
{
    public class PixPayment: IProvedorPagamento
    {

        public PixPayment()
        { 
        }
        public virtual async Task<Order> ProcessPayment(decimal paymentValue, int customerId)
        {
            return await Task.FromResult(new Order()
            {
                // melhorar essa consulta parta retonar o cliente
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.Now,
            });

        }
    }
}
