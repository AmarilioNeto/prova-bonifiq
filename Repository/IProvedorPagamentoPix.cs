using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public interface IProvedorPagamentoPix 
    {     
        Task<Order> ProcessPaymentPix(decimal paymentValue, int customerId);
       
        
    }
}
