using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services.PaymentForm
{
    public class PixPayment :IProvedorPagamentoPix
    {
           
        public PixPayment()
        {         
        }
        public virtual async Task<Order> ProcessPaymentPix(decimal paymentValue, int customerId)
        {
            // aqui fica a implementação espefico do tipo de pagamento pix.Aqui tem apena uma generica.  
            return await Task.FromResult(new Order()
            {              
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                
            });
            
        }
       
    }
}
