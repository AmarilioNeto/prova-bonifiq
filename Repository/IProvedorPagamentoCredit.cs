using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public interface IProvedorPagamentoCredit
    {
        Task<Order> ProcessPaymentCredit(decimal paymentValue, int customerId);
    }
}
