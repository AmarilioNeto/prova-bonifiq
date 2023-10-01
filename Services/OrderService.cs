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
                //melhorar esse codigo para switch case e tentar implemntar enun
                return await _provedorPagamento.ProcessPayment(paymentValue, customerId);
            }
            if(paymentMethod == "creditcard")
            {
                return await _provedorPagamento.ProcessPayment(paymentValue, customerId);
            }
            if(paymentMethod == "paypal")
            {
                return await _provedorPagamento.ProcessPayment(paymentValue, customerId);
            }
            else
            {
                throw new ArgumentException("Método de pagamento não encontrado");
            }
            

        }

    }
}
