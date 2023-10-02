using ProvaPub.Models;
using ProvaPub.Repository;


namespace ProvaPub.Services
{
    public class OrderService
    {
        private readonly IProvedorPagamentoPix _provedorPagamentoPix;
        private readonly IProvedorPagamentoCredit _provedorPagamentoCredit;
        private readonly IProvedorPagamentoPaypal _provedorPagamentoPaypal;

        public OrderService(IProvedorPagamentoPix provedorPagamentoPix, 
                           IProvedorPagamentoCredit provedorPagamentoCredit,
                           IProvedorPagamentoPaypal provedorPagamentoPaypal)
        {
            _provedorPagamentoPix = provedorPagamentoPix;
            _provedorPagamentoCredit = provedorPagamentoCredit;
            _provedorPagamentoPaypal = provedorPagamentoPaypal;
        }

        public async Task<Order> PayOrder( string paymentMethod, decimal paymentValue, int customerId)
        {

            IProvedorPagamentoPix paymentProvider;

            switch (paymentMethod.ToLower())
            {
                case "pix":
                    return await _provedorPagamentoPix.ProcessPaymentPix(paymentValue, customerId);
                    break;
                case "credit":
                    return await _provedorPagamentoCredit.ProcessPaymentCredit(paymentValue, customerId);
                    break;
                case "paypal":
                    return await _provedorPagamentoPaypal.ProcessPaymentPayPal(paymentValue, customerId);
                    break;
                default:
                    throw new ArgumentException("Método de pagamento não encontrado");
            }



        }


    }
}
