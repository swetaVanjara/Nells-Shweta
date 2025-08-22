using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using NellsPay.Send.Models.MoneyTransferFlowModels;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.PopUpPages;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using static NellsPay.Send.Messages.WeakMessages;

namespace NellsPay.Send.ViewModels;

[QueryProperty(nameof(ChangedCurrencyCheck), "ChangedCurrencyCheck")]
[QueryProperty(nameof(CurrencyChange), "CurrencyChange")]
public partial class SendMoneyViewModel : BaseViewModel
{
    private readonly IFxService _FxService;
    private readonly ICurrencyTransferService _currencyTransferService;
    private readonly ISettingsProvider _SettingsProvider;

    [ObservableProperty] private double moneySend;
    [ObservableProperty] private double moneyRecive;
    [ObservableProperty] private string checkSendRecive;
    [ObservableProperty] private Currency currencyChange;
    [ObservableProperty] private string changedCurrencyCheck;
    [ObservableProperty] private Currency currencySend = new();
    [ObservableProperty] private Currency currencyRecive = new();
    [ObservableProperty] private ObservableCollection<string> deliveryMethodList = new();
    [ObservableProperty] private bool fees = false;
    [ObservableProperty] private bool isUpdateValue = false;
    [ObservableProperty] private double feesAmount;
    [ObservableProperty] private string exchangeRateFrom;
    [ObservableProperty] private double exchangeRate;
    [ObservableProperty] private string exchangeRateTo;
    [ObservableProperty] private string deliveryMethod;
    [ObservableProperty] private bool sendMoneyVisible;
    [ObservableProperty] private bool isBottomSheet;
    [ObservableProperty] private string selectedPayment;

    public SendMoneyViewModel(IFxService fxService, ICurrencyTransferService currencyTransferService, ISettingsProvider settingsProvider)
    {
        _FxService = fxService;
        _currencyTransferService = currencyTransferService;
        _SettingsProvider = settingsProvider;
        SetInitialData();

        WeakReferenceMessenger.Default.Register<ChangeCurrencyMessage>(this, (r, m) =>
        {
            if (m.Value.Item2 == "Change Sending Currency")
                CurrencySend = m.Value.Item1;
            else if (m.Value.Item2 == "Change Receiving Currency")
                CurrencyRecive = m.Value.Item1;

            _ = LoadExchangeRateAsync();
        });
    }

    partial void OnChangedCurrencyCheckChanged(string value)
    {
        CheckSendRecive = value;
    }

    partial void OnCurrencyChangeChanged(Currency value)
    {
        if (CheckSendRecive == "Send")
            CurrencySend = value;
        else if (CheckSendRecive == "Receive")
        {
            CurrencyRecive = value;
            _currencyTransferService.Reciever = value;
            DeliveryMethodList = GetDeliveryMethodList();
            DeliveryMethod = DeliveryMethodList.FirstOrDefault();
        }
        _ = LoadExchangeRateAsync();
    }

    partial void OnMoneySendChanged(double value)
    {
        double sendAmount = ConvertToDouble(value.ToString());
        double rate = ConvertToDouble(ExchangeRate.ToString());
        RecivingChange(sendAmount, rate);
    }
    partial void OnMoneyReciveChanged(double value)
    {
        double receivingAmount = ConvertToDouble(value.ToString());
        double rate = ConvertToDouble(ExchangeRate.ToString());
        SendingChange(receivingAmount,rate);
    }

    public void CheckUserIsVerifiedOrNot()
    {
        SendMoneyVisible = _SettingsProvider.Verifyidentity;
    }

    private void SetInitialData()
    {
        DeliveryMethodList = new ObservableCollection<string>();
        CurrencySend = _currencyTransferService.Sender ?? new Currency();
        CurrencyRecive = _currencyTransferService.Reciever ?? new Currency();
        MoneySend = 100.00;
        Fees = false;
        _ = LoadExchangeRateAsync();
        CheckUserIsVerifiedOrNot();
        DeliveryMethodList = GetDeliveryMethodList();
        SelectedPayment = DeliveryMethodList.FirstOrDefault();
    }

    private ObservableCollection<string> GetDeliveryMethodList()
    {
        var deliveryList = CurrencyRecive?.paymentMethods?
        .Where(pm => pm.status?.Equals("Active", StringComparison.OrdinalIgnoreCase) == true)
        .Select(pm => pm.name)
        .ToList() ?? [];

        if (deliveryList.Count == 0)
        {
            deliveryList = ["Mobile Money"];
        }
        return new ObservableCollection<string>(deliveryList);
    }

    private static double ConvertToDouble(string amount)
    {
        if (string.IsNullOrEmpty(amount)) return 0;
        amount = amount.Replace(",", "").Replace("RP", "").Replace("$", "").Trim();
        return double.TryParse(amount, NumberStyles.Any, CultureInfo.InvariantCulture, out double result) ? result : 0;
    }

    private async Task LoadExchangeRateAsync()
    {

        double amount = ConvertToDouble(MoneySend.ToString());
        if (amount <= 0) return;

        try
        {
            var fxResult = await _FxService.ConvertCurrency(CurrencySend.CurrencyCode, CurrencyRecive.CurrencyCode, amount, forceRefresh: true);
            if (fxResult?.FxConvert != null)
            {
                ExchangeRate = Math.Round(fxResult.FxConvert.Rate, 2);
                MoneyRecive = Math.Round(fxResult.FxConvert.ConvertedAmount, 2);
                ExchangeRateFrom = CurrencySend.CurrencyCode;
                ExchangeRateTo = CurrencyRecive.CurrencyCode;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SendMoneyViewModel] ExchangeRate Error: {ex.Message}");
        }
    }

    private async void RecivingChange(double sendAmount, double exchangeRate)
    {
        if (IsUpdateValue) return;

        IsUpdateValue = true;
        MoneyRecive = Math.Round(sendAmount * exchangeRate, 2);
        IsUpdateValue = false;
    }
    private async void SendingChange(double receivingAmount, double exchangeRate)
    {
        if (IsUpdateValue) return;

        IsUpdateValue = true;
        MoneySend = Math.Round(receivingAmount / exchangeRate, 2);
        IsUpdateValue = false;
    }

    [RelayCommand]
    private async Task Next()
    {
        var transferFee = 0.00;
        var bankFee = "1.25";

        _currencyTransferService.SelectedPaymentMethod = null;
        _currencyTransferService.SaltbyemailResponse = null;

        var reviewModel = new ReviewTransictionModel
        {
            SendISOCode = CurrencySend.CurrencyCode,
            ReciveISOCode = CurrencyRecive.CurrencyCode,
            Transferamount = MoneySend,
            Reciveamount = MoneyRecive,
            Totaltorecipient = MoneyRecive.ToString("N", CultureInfo.InvariantCulture)
                   + " " + CurrencyRecive.CurrencyCode,
            Exchangerate = ExchangeRate,
            Transferfee = transferFee,
            Transfertaxes = "$0",
            Bankfee = bankFee,
            Voucher = "",
            Total = ConvertToDouble(MoneySend.ToString()) + " " + CurrencySend.CurrencyCode,
            Recipient = new Recipient(),
            DeliveryMethod = SelectedPayment,
            Transfertime = "Within minutes"
        };

        _currencyTransferService.ReviewTransictionData = reviewModel;
        await Shell.Current.GoToAsync($"Recipients/Money?isFromMoney=true");
    }

    [RelayCommand]
    private async Task Send()
    {
        await Shell.Current.GoToAsync($"{nameof(SendingReceivingCurrencyPage)}",
            new Dictionary<string, object>
            {
                ["currency"] = CurrencySend,
                ["title"] = "Sending Currency"
            });
    }

    [RelayCommand]
    private async Task Recieve()
    {
        await Shell.Current.GoToAsync($"{nameof(SendingReceivingCurrencyPage)}",
            new Dictionary<string, object>
            {
                ["currency"] = CurrencyRecive,
                ["title"] = "Receiving Currency"
            });
    }
    [RelayCommand]
    private async Task OpenPopup()
    {
        IsBottomSheet = true;
    }
    [RelayCommand]
    private async Task SelectBottomSheet(string SelectedItem)
    {
        SelectedPayment = SelectedItem;
        IsBottomSheet = false;
    }

    [RelayCommand]
    private async Task BottomSheetBack()
    {
        IsBottomSheet = false;
    }
}