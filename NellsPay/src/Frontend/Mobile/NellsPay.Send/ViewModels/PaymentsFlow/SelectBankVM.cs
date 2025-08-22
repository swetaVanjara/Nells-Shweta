using NellsPay.Send.Models.MoneyTransferFlowModels;
using NellsPay.Send.ResponseModels;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.PaymentsFlow
{
    [QueryProperty(nameof(TransferPin), "TransferPin")]
    [QueryProperty(nameof(TransactionNumber), "transactionNumber")]
    [QueryProperty(nameof(CustomerSeId), "customerSeId")]
    public partial class SelectBankVM : BaseViewModel
    {
        #region Dependencies
        private readonly IToastService _toastService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly IPaymentFlowService _paymentService;
        private readonly ICurrencyTransferService _currencyTransferService;
        #endregion
        private const int PageSize = 10;

        #region Observable Properties
        [ObservableProperty] private bool isLoading;

        [ObservableProperty] private bool isLoadingMore;
        [ObservableProperty] private int itemsPerPage = PageSize;
        [ObservableProperty] private int currentIndex = 0;
        [ObservableProperty] private bool hasMoreData = true;
        [ObservableProperty] private string transactionNumber = string.Empty;
        [ObservableProperty] private string customerSeId = string.Empty;
        [ObservableProperty] private TransferPinModel transferPin = new();
        [ObservableProperty] private ObservableCollection<Datas> bankList = new();
        [ObservableProperty] private string search = string.Empty;

        List<Datas> allBanks = new();
        #endregion

        public SelectBankVM(
            IToastService toastService,
            ISettingsProvider settingsProvider,
            IPaymentFlowService paymentService,
            ICurrencyTransferService currencyTransferService)
        {
            _settingsProvider = settingsProvider;
            _currencyTransferService = currencyTransferService;
            _paymentService = paymentService;
            _toastService = toastService;
            Task.Run(async () =>
            {
                await GetDataAsync();
            });
        }
        public async Task GetDataAsync()
        {
            try
            {
                IsLoading = true;

                // var listData = await _paymentService.GetPaymentProvider(_settingsProvider.country2Code, 0, 100); 
                var listData = await _paymentService.GetPaymentProvider("XF", 0, 100); //XF should be replaced by sender country code (_settingsProvider.country2Code)

                if (listData?.Providers?.Data != null)
                {
                    allBanks = listData?.Providers?.Data.ToList();
                    BankList = new ObservableCollection<Datas>(allBanks!.Take(9).ToArray());
                    CurrentIndex = ItemsPerPage;
                    HasMoreData = CurrentIndex < allBanks.Count;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void SearchBank()
        {
            if (string.IsNullOrWhiteSpace(Search))
            {
                BankList = new ObservableCollection<Datas>(allBanks.Take(9));
            }
            else
            {
                var filtered = allBanks
                        .Where(b => !string.IsNullOrWhiteSpace(b.Name) &&
                                    b.Name.Contains(Search, StringComparison.OrdinalIgnoreCase))
                        .Take(9)
                        .ToList();

                BankList = new ObservableCollection<Datas>(filtered);
            }
        }
        public async Task<string> GetPublicIPAddressAsync()
        {
            try
            {
                using var httpClient = new HttpClient();
                return await httpClient.GetStringAsync("https://api.ipify.org");
            }
            catch
            {
                return "Unable to fetch";
            }
        }

        [RelayCommand]
        private async Task LazyLoader()
        {
            if (!HasMoreData || IsLoadingMore)
                return;

            IsLoadingMore = true;

            await Task.Delay(800);

            var nextBatch = allBanks.Skip(CurrentIndex).Take(ItemsPerPage).ToList();

            foreach (var item in nextBatch)
                BankList.Add(item);

            CurrentIndex += nextBatch.Count;
            HasMoreData = CurrentIndex < allBanks.Count;

            IsLoadingMore = false;
        }

        [RelayCommand]
        private async Task Selected(Datas item)
        {
            try
            {
                IsLoading = true;
                var reviewData = _currencyTransferService.ReviewTransictionData;
                var senderData = _currencyTransferService.Sender;

                if (reviewData == null || senderData == null)
                {
                    return;
                }

                DevicePlatform platform = DeviceInfo.Current.Platform; // iOS, Android
                var publicIP = await GetPublicIPAddressAsync();

                var obj = new PaymentInitiationWrapper
                {
                    paymentInitiationRequest = new PaymentInitiationRequest
                    {
                        deviceOs = platform.ToString(),
                        deviceIpAddress = publicIP,
                        transactionNumber = TransactionNumber,
                        amount = "8.00",
                        providerCode = item.Code,
                        templateIdentifier = senderData.CurrencyCode switch
                        {
                            "GBP" => "FPS",
                            "EUR" => "SEPA_INSTANT",
                            _ => string.Empty
                        },
                        customerId = CustomerSeId,
                        countryCode = senderData.Country2Code,
                    }
                };
                Debug.WriteLine(obj.ToString());
                var createPaymentResult = await _paymentService.CreatePayment(obj);
                if (createPaymentResult != null)
                {
                    var paymentUrl = createPaymentResult.paymentResponse.paymentUrl;
                    var encodedUrl = Uri.EscapeDataString(paymentUrl);
                    Debug.WriteLine(paymentUrl);
                    Debug.WriteLine(encodedUrl);
                    var PaymentId = createPaymentResult.paymentResponse.paymentId;
                    await Shell.Current.GoToAsync($"{nameof(PaymentWebview)}?urlWebView={encodedUrl}&paymentId={PaymentId}");
                }
                // await Shell.Current.GoToAsync($"{nameof(SelectBankAccountPage)}?",
                // new Dictionary<string, object>
                // {
                //     ["Bank"] = item,
                //     ["TransferPin"] = TransferPin,
                // });
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task BackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
