using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using NellsPay.Send.Helpers;

namespace NellsPay.Send.ViewModels.EditProfileVM
{
    [QueryProperty(nameof(User), "UserEdit")]
    public partial class EditProfileVM : BaseViewModel
    {
        private Action? _openPickerAction;
        private readonly ISettingsProvider _settingsProvider;
        private readonly ICountriesService _countriesService;
        private readonly ICustomerService _customerService;
        [ObservableProperty] private bool notification;
        [ObservableProperty] private string dialCode;
        [ObservableProperty] private bool isBusy;
        [ObservableProperty] private UserModel user;
        [ObservableProperty] private UserModel checkUser;
        [ObservableProperty] private List<CountryCodes> countries;
        [ObservableProperty] private CountryCodes selectedCountry;
        [ObservableProperty] private ResponseModels.Countries countriesList;

        public EditProfileVM(ISettingsProvider settingsProvider,ICountriesService countriesService, ICustomerService customerService)
        {
            _customerService = customerService;
            _countriesService = countriesService;
            _settingsProvider = settingsProvider;
            User = new UserModel();
            Task.Run(async () =>
          {
              IsBusy = true;
              await GetCountryList();
              IsBusy = false;
          });
        }
        partial void OnSelectedCountryChanged(CountryCodes value)
        {
            User.Country = value.Name ?? User.Country;
            DialCode = value.DialCode;
            SelectedCountry.Flag = value.Flag;
        }
        public void RegisterOpenPickerAction(Action action)
        {
            _openPickerAction = action;
        }
        public async Task GetUserInfo()
        {
            try
            {
                IsBusy = true;
                var user = await _customerService.GetCustomerByEmail(_settingsProvider.Email);
                if (user != null)
                {
                    var customer = user?.Customer;
                    User = new UserModel()
                    {
                        Email = customer?.Email ?? "",
                        UserFirstName = customer?.FirstName ?? "",
                        UserLastName = customer?.LastName ?? "",
                        Address = customer?.AddressLine1 ?? "",
                        PhoneNumber = customer?.PhoneNumber ?? "",
                        UserImage = "usertest.png",
                        BirthDate = customer != null
                                ? DateOnly.FromDateTime(customer.DateOfBirth)
                                : DateOnly.FromDateTime(DateTime.UtcNow),
                        City = customer?.City,
                        Region = customer?.Region,
                        PostCode = customer?.PostCode,
                        Country = customer?.Country,
                    };
                    CheckUser = User;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }

        }
        public async Task GetCountryList()
        {
            var countriesResponse = await _countriesService.GetCountries(0,70);
            countriesList = new ResponseModels.Countries
            {
                data = countriesResponse?.countries?.data
            };
            Countries = new List<CountryCodes>();
            foreach (var item in countriesResponse?.countries?.data)
            {
                Countries.Add(new CountryCodes
                {
                    DialCode = item.phoneCode,
                    Flag = item.currencyFlag,
                    Name = item.countryName,
                    ISO = item.country2Code.ToString(),
                });
            }
            SelectedCountry = Countries.Where(s => s.ISO == _settingsProvider.country2Code).FirstOrDefault() ?? Countries.First();
        }
        public async Task EditSave()
        {

            DateTimeOffset dto = DateTimeOffset.ParseExact(User.BirthDate.ToString(), "dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture);
            var matchedCountry = countriesList.data?
                .FirstOrDefault(c =>
                    string.Equals(c.countryName, User.Country, StringComparison.OrdinalIgnoreCase));
            // Convert to UTC and format as ISO 8601
            string isoString = dto.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            var obj = new EditCustomerWrapper
            {
                Customer = new Customer
                {
                    Id = Guid.Parse(_settingsProvider.CustomerId),
                    UserId = Guid.Parse(_settingsProvider.UserId),
                    FirstName = User.UserFirstName,
                    LastName = User.UserLastName,
                    Email = User.Email,
                    AddressLine1 = User.Address,
                    AddressLine2 = "",
                    PhoneNumber = User.PhoneNumber,
                    DateOfBirth = isoString,
                    City = User.City,
                    Country = User.Country ?? "FRANCE",
                    Country2Code = matchedCountry != null ? matchedCountry.country2Code : "FR",
                    Gender = "female",
                    PostCode = User.PostCode,
                    Region = User.Region,
                    Initials = "",
                    Status = "Inactive",
                }
            };
            Debug.Write(obj.ToString());
            var response = await _customerService.EditCustomer(obj);
            if (response != null)
            {
                var user = await _customerService.GetCustomerByEmail(User.Email);
                _settingsProvider.AddressLine = user.Customer.AddressLine1;
                _settingsProvider.City = user.Customer.City;
                _settingsProvider.Country = user.Customer.Country;
                _settingsProvider.country2Code = user.Customer.country2Code;
                _settingsProvider.Gender = user.Customer.Gender;
                _settingsProvider.DateOfBirth = user.Customer.DateOfBirth;
                _settingsProvider.PostCode = user.Customer.PostCode;
                _settingsProvider.Region = user.Customer.Region;
                _settingsProvider.PhoneNumber = user.Customer.PhoneNumber;
                _settingsProvider.LastName = user.Customer.LastName;
                WeakReferenceMessenger.Default.Send<object, string>(this, HomePageRefreshMessage.UpdateData);


            }
            var SavedItemSelected = new Dictionary<string, object>
                {
                    { "SavedItem", User }
                };
            await Shell.Current.GoToAsync("..", true, SavedItemSelected);


        }

        #region RelayCommands

        [RelayCommand]
        private async Task SaveProfileEdit()
        {
            // if (CheckUser.Email == User.Email ||
            //     CheckUser.UserFirstName == User.UserFirstName ||
            //     CheckUser.UserLastName == User.UserLastName ||
            //     CheckUser.Address == User.Address ||
            //     CheckUser.PhoneNumber == User.PhoneNumber ||
            //     CheckUser.UserImage == User.UserImage ||
            //     CheckUser.BirthDate == User.BirthDate)
            // {
            //     await EditSave();
            // }
            await EditSave();
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        private void OpenPicker()
        {
            _openPickerAction?.Invoke();
        }
        #endregion
    }
}
