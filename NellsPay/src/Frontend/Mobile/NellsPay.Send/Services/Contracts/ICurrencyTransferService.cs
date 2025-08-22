using NellsPay.Send.Models.MoneyTransferFlowModels;

namespace NellsPay.Send.Services.Contracts
{
    public interface ICurrencyTransferService
    {
        Currency? Sender { get; set; }
        Currency? Reciever { get; set; }
        ReviewTransictionModel? ReviewTransictionData { get; set; }
        PaymentMethod? SelectedPaymentMethod { get; set; }
        ResponseModels.SaltbyemailResponse? SaltbyemailResponse { get; set; }
        SendingReasonModel? sendingReasonModel { get; set; }
    }
}
