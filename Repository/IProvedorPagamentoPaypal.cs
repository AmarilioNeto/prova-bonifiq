using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public interface IProvedorPagamentoPaypal
    {
        Task<Order> ProcessPaymentPayPal(decimal paymentValue, int customerId);
    }
}
