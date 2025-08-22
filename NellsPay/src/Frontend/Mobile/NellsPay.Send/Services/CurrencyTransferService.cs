using NellsPay.Send.Models.MoneyTransferFlowModels;

namespace NellsPay.Send.Services
{
    public class CurrencyTransferService : ICurrencyTransferService
    {
        public Currency? Sender { get; set; }
        public Currency? Reciever { get; set; }
        public ReviewTransictionModel? ReviewTransictionData { get; set; }
        public PaymentMethod? SelectedPaymentMethod { get; set; }
        public ResponseModels.SaltbyemailResponse? SaltbyemailResponse { get; set; }
        public SendingReasonModel? sendingReasonModel { get; set; }
    }
}