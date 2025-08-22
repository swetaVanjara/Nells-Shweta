using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using NellsPay.Send.Messages;
using NellsPay.Send.Models.RecipientsModels;
using NellsPay.Send.ResponseModels;

namespace NellsPay.Send.ViewModels.RecipientsViewModels
{
    [QueryProperty(nameof(PageTitle), "pageTitle")]
    public partial class ChooseDeliveryMethodVM : BaseViewModel
    {
        private readonly ICountriesService _APICountriesService;
        [ObservableProperty] private ObservableCollection<PaymentMethod>? paymentMethods;
        [ObservableProperty] private AddEditRecipientModel recipientInfo;
        [ObservableProperty] private string pageTitle;
        [ObservableProperty] private string currentPageName;
        [ObservableProperty] private string countryID;
        [ObservableProperty] private bool isLoading;

        public ChooseDeliveryMethodVM(ICountriesService countriesService)
        {
            _APICountriesService = countriesService;
            RecipientInfo = new AddEditRecipientModel();
            PaymentMethods ??= new ObservableCollection<PaymentMethod>();
        }
        public async Task OnPageAppearing()
        {

        }
        partial void OnPageTitleChanged(string value)
        {
            Task.Run(async () => await GetData(value));
        }
        public async Task GetData(string IsCheckPage)
        {
                CurrentPageName = IsCheckPage;
                if (IsCheckPage.Equals("Choose A Delivery Method", StringComparison.Ordinal))
                    await DeliveryMethod();
                else if (IsCheckPage.Equals("Choose A Bank", StringComparison.Ordinal))
                    await BankData();
                else if (IsCheckPage.Equals("Choose Mobile Provider", StringComparison.OrdinalIgnoreCase))
                    await MobileData();
        }

        public async Task DeliveryMethod()
        {
            try
            {
                IsLoading = true;
                if (TempRecipientStore.SelectedCountry?.CurrencyName == null)
                {
                    var countrieList = await _APICountriesService.GetCountries(0,70);
                    foreach (var item in countrieList.countries.data)
                    {
                        foreach (var paymentitem in item.paymentMethods)
                        {
                            if (paymentitem.status.Equals("Active", StringComparison.Ordinal))
                            {
                                PaymentMethods.Add(new PaymentMethod
                                {
                                    id = paymentitem.id,
                                    countryId = paymentitem.countryId,
                                    name = paymentitem.name,
                                    status = paymentitem.status,
                                    description = paymentitem.description,
                                });
                            }
                        }
                    }
                    return;
                }
                var data = TempRecipientStore.PaymentMethodList;
                if (data != null)
                {
                    var activeProviders = data.Where(item => item.status == "Active").ToList();
                    if (activeProviders.Count == 0)
                    {
                        PaymentMethods.Add(new PaymentMethod
                        {
                            id = "2",
                            name = "Bank Transfer",
                            status = "Active",
                            description = "Bank Transfer",
                        });
                    }
                    else
                    {
                        foreach (var item in activeProviders)
                        {
                            PaymentMethods.Add(item);
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                IsLoading = false;
            }
        }


        public async Task BankData()
        {
            try
            {
                IsLoading = true;
                if (TempRecipientStore.SelectedCountry?.Id != null && TempRecipientStore.SelectedCountry.Id != Guid.Empty)
                {
                    CountryID = TempRecipientStore.SelectedCountry.Id.ToString();
                }
                else
                {
                    var countrieList = await _APICountriesService.GetCountries(0,70);
                    CountryID = countrieList.countries.data.FirstOrDefault(c => c.countryName == TempRecipientStore.SelectedCountry.Country)?.id;
                }
                // Bank Transfer logic
                var data = await _APICountriesService.GetFinancialInstitutions(CountryID);
                if (data.financialInstitutions != null)
                {
                    foreach (var item in data.financialInstitutions)
                    {
                        PaymentMethods.Add(new PaymentMethod
                        {
                            id = item.id,
                            name = item.name,
                            status = item.status,
                            countryId = item.countryId,
                            description = item.description,
                        });
                    }
                }
            }
            catch (System.Exception)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task MobileData()
        {
            try
            {
                IsLoading = true;
                if (TempRecipientStore.SelectedCountry?.Id != null && TempRecipientStore.SelectedCountry.Id != Guid.Empty)
                {
                    CountryID = TempRecipientStore.SelectedCountry.Id.ToString();
                }
                else
                {
                    var countrieList = await _APICountriesService.GetCountries(0,70);
                    CountryID = countrieList.countries.data.FirstOrDefault(c => c.countryName == TempRecipientStore.SelectedCountry.Country)?.id;
                }
                // Mobile Money logic
                var data = await _APICountriesService.GetMobileWalletProviders(CountryID);
                if (data.mobileWalletProviders != null)
                {
                    foreach (var item in data.mobileWalletProviders)
                    {
                        PaymentMethods.Add(new PaymentMethod
                        {
                            id = item.id,
                            name = item.name,
                            status = item.status,
                            countryId = item.countryId,
                            description = item.description,
                        });
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            try
            {
                WeakReferenceMessenger.Default.Send(new WeakMessages("Selected Payment"));
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
        [RelayCommand]
        private async Task SelectDeliveryMethod(PaymentMethod SelectedPaymentMethod)
        {
            if (CurrentPageName.Equals("Choose A Delivery Method", StringComparison.Ordinal))
                WeakReferenceMessenger.Default.Send(new ValueChangedMessage<(string Name, int CheckPayment)>((SelectedPaymentMethod.name, 1)));
            else
                WeakReferenceMessenger.Default.Send(new ValueChangedMessage<(string Name, int CheckPayment)>((SelectedPaymentMethod.name, 2)));
            await Shell.Current.GoToAsync("..");
        }
    }
}