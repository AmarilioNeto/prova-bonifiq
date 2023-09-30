using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public interface IProvedorPagamento
    {
        Task<Order> ProcessPayment(decimal paymentValue, int customerId);
    }
}
