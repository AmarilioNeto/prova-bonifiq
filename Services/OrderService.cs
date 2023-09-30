using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.PaymentForm;

namespace ProvaPub.Services
{
    public class OrderService
    {
        private readonly IProvedorPagamento _provedorPagamento;

        public OrderService(IProvedorPagamento provedorPagamento)
        {
            _provedorPagamento = provedorPagamento;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {

            IProvedorPagamento paymentProvider;
            if (paymentMethod == "pix")
            {
                paymentProvider = new PixPayment();
            }
            if(paymentMethod == "creditcard")
            {
                paymentProvider = new CreditPayment();
            }
            if(paymentMethod == "paypal")
            {
                paymentProvider = new PaypalPayment();
            }
            else
            {
                throw new ArgumentException("Método de pagamento não encontrado");
            }
            var orderService = new OrderService(paymentProvider);           
            return await _provedorPagamento.ProcessPayment(paymentValue, customerId);

        }

    }
}
