using NellsPay.Send.ResponseModels;

public class RecipientDataStore
{
    public Recipient? NewlyAddedRecipient { get; set; }
}


public static class TempRecipientStore
{
    public static (Recipient, string)? AddEditRecipient { get; set; }
    public static List<PaymentMethod>? PaymentMethodList { get; set; }
    public static Currency SelectedCountry { get; set; }
}