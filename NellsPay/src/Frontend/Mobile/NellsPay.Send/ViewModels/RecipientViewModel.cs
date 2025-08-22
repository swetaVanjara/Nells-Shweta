using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using NellsPay.Send.Models.MoneyTransferFlowModels;
using NellsPay.Send.Models.RecipientsModels;
using NellsPay.Send.Repository;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.PopUpPages;
using NellsPay.Send.Views.RecipientsPages;
using System.ComponentModel;
using System.Windows.Input;
using static NellsPay.Send.Messages.WeakMessages;


namespace NellsPay.Send.ViewModels
{
    [QueryProperty(nameof(IsFromMoney), "isFromMoney")]
    public partial class RecipientViewModel : BaseViewModel
    {
        #region Dependencies
        private readonly ICurrencyTransferService _currencyTransferService;
        private readonly RecipientDataStore _recipientDataStore;
        private readonly IRecipientService _recipientService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly IToastService _toastService;
        private readonly IRecipientRepository _databaseService;
        #endregion

        #region Observable Properties
        [ObservableProperty] private bool isFromMoney;
        [ObservableProperty] private string search = string.Empty;
        [ObservableProperty] private string pageName;
        [ObservableProperty] private bool isBackButton;
        [ObservableProperty] private bool isLoading;
        [ObservableProperty] private ObservableCollection<Grouping<string, Recipient?>> groupRecipients = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasFavorites))]
        private ObservableCollection<Recipient?> favorites = new();
        public bool HasFavorites => Favorites?.Any() == true;
        partial void OnFavoritesChanged(ObservableCollection<Recipient?> value)
        {
            if (value != null)
                value.CollectionChanged += (s, e) => OnPropertyChanged(nameof(HasFavorites));
        }

        [ObservableProperty] private List<Recipient?> recipients = new();
        [ObservableProperty] private Recipient recipientData;
        #endregion

        public RecipientViewModel(
            ICurrencyTransferService currencyTransferService,
            ISettingsProvider settingsProvider,
            IToastService toastService,
            RecipientDataStore recipientDataStore,
            IRecipientRepository databaseService,
            IRecipientService recipientService)
        {
            _databaseService = databaseService;
            _currencyTransferService = currencyTransferService;
            _settingsProvider = settingsProvider;
            _toastService = toastService;
            _recipientDataStore = recipientDataStore;
            _recipientService = recipientService;
        }

        partial void OnIsFromMoneyChanged(bool value)
        {
            IsBackButton = value;
            Task.Run(async () => await OnAppearingAsync());
        }
        

        private async Task NavigateToEditRecipient(Recipient? recipient)
        {
            if (recipient?.Id == null) return;

            if (!IsBackButton)
            {
                TempRecipientStore.AddEditRecipient = null;
                TempRecipientStore.AddEditRecipient = (recipient, "Edit Recipient");
                TempRecipientStore.PaymentMethodList = new();
                TempRecipientStore.SelectedCountry = new();
                await Shell.Current.GoToAsync(nameof(AddEditRecipientPage));
            }
            else
            {
                _currencyTransferService.ReviewTransictionData.Recipient = recipient;
                await Shell.Current.GoToAsync(nameof(ReviewTransictionPage));
            }
        }

        public async Task GetData()
        {
            try
            {
                IsLoading = true;

                if (_settingsProvider.CustomerId == null)
                {
                    _toastService.ShowToast("Failed to get customer Id.");
                    return;
                }

                var data = await _recipientService.GetRecipientsAsync();
                if (data?.Recipients != null)
                {
                    var filtered = ApplyPageFilter(data?.Recipients).ToList();
                    Recipients = filtered.ToList();
                    var favRecipientList = await _databaseService.GetFavoriteRecipientAsync();
                    var filteredFavs = ApplyPageFilter(favRecipientList).Where(r => r?.IsFavorite == true);
                    Favorites = new ObservableCollection<Recipient?>(filteredFavs.ToList());
                    foreach (var (recipient, favItem) in from fav in Favorites
                                                         join rec in Recipients on fav.Id equals rec.Id
                                                         select (rec, fav))
                    {
                        recipient.IsFavorite = favItem.IsFavorite;
                    }
                    GroupRecipientsByFirstNameFirstLetter(Recipients);
                }
                else
                {
                    Debug.WriteLine("Data not set");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching data: {ex.Message}");
                _toastService.ShowToast("Failed to load recipients.");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool IsRecipientsPage => string.Equals(PageName, "Recipients", StringComparison.OrdinalIgnoreCase);

        private (string? country, string? method) GetPageFilters()
        {
            return (_currencyTransferService?.Reciever?.Country,
                    _currencyTransferService?.ReviewTransictionData?.DeliveryMethod);
        }

        private IEnumerable<Recipient?> ApplyPageFilter(IEnumerable<Recipient?> source)
        {
            if (IsRecipientsPage) return source;
            var (country, method) = GetPageFilters();
            return source.Where(r =>
                string.Equals(r?.Country,  country, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(r?.PayOutType, method,  StringComparison.OrdinalIgnoreCase));
        }

        private void GroupRecipientsByFirstNameFirstLetter(IEnumerable<Recipient?> recipients)
        {
            if (recipients == null || !recipients.Any())
            {
                GroupRecipients = new ObservableCollection<Grouping<string, Recipient?>>();
                return;
            }

            foreach (var item in recipients)
            {
                if (!string.IsNullOrEmpty(item?.Initials))
                    item.Initials = item.Initials.ToUpper();
            }

            var sorted = recipients
                .Where(r => !string.IsNullOrWhiteSpace(r?.FirstName))
                .OrderBy(r => r.FirstName)
                .GroupBy(r => r.FirstName![0].ToString().ToUpper())
                .Select(g => new Grouping<string, Recipient?>(g.Key, g));

            GroupRecipients = new ObservableCollection<Grouping<string, Recipient?>>(sorted);
        }

        public async Task OnAppearingAsync()
        {
            try
            {
                if (_recipientDataStore.NewlyAddedRecipient != null)
                {
                    var existing = Recipients.FirstOrDefault(r => r?.Id == _recipientDataStore.NewlyAddedRecipient.Id);
                    if (existing != null)
                    {
                        int index = Recipients.IndexOf(existing);
                        Recipients[index] = _recipientDataStore.NewlyAddedRecipient;
                    }
                    else
                    {
                        Recipients.Add(_recipientDataStore.NewlyAddedRecipient);
                    }

                    GroupRecipientsByFirstNameFirstLetter(Recipients);
                }
                else
                {
                    PageName = IsBackButton ? "Choose recipients" : "Recipients";
                    IsBackButton = PageName != "Recipients";
                    await GetData();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [RelayCommand]
        private async Task NavigateToDetail(Recipient recipient)
        {
            await NavigateToEditRecipient(recipient);
        }


        [RelayCommand]
        private async Task AddRecipients()
        {
            if (_settingsProvider.CustomerId == null)
            {
                _toastService.ShowToast("Failed to get customer Id.");
                return;
            }

            TempRecipientStore.AddEditRecipient = (new Recipient(), "New Recipient");
            await Shell.Current.GoToAsync($"{nameof(SelectCountryRecipient)}?routePageNav={IsBackButton}");
        }

        [RelayCommand]
        private async Task ToggleFavorite(Recipient item)
        {
            try
            {
                if (item == null) return;

                item.IsFavorite = !item.IsFavorite;
                if (item.IsFavorite)
                {
                    var res = await _databaseService.FavoriteRecipientAsync(item);
                }
                else
                {
                    var res = await _databaseService.UnFavoriteRecipientAsync(item);
                }
                var RecipientsList = await _databaseService.GetFavoriteRecipientAsync();
                var filteredFavs = ApplyPageFilter(RecipientsList).Where(x => x?.IsFavorite == true);
                Favorites = new ObservableCollection<Recipient?>(filteredFavs.ToList());
                
                foreach (var (recipient, favItem) in from fav in Favorites
                                                     join rec in Recipients on fav.Id equals rec.Id
                                                     select (rec, fav))
                {
                    recipient.IsFavorite = favItem.IsFavorite;
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void SearchRecipient()
        {
            if (string.IsNullOrWhiteSpace(Search))
            {
                GroupRecipientsByFirstNameFirstLetter(Recipients);
                Task.Run(async () =>
                {
                    var recipientsList = await _databaseService.GetFavoriteRecipientAsync();
                    Favorites = new ObservableCollection<Recipient?>(recipientsList.Where(x => x?.IsFavorite == true));
                });
                return;
            }

            string searchTerm = Search.Trim().ToLower();

            var filtered = Recipients
                .Where(r =>
                    (!string.IsNullOrWhiteSpace(r?.FirstName) && r.FirstName.ToLower().Contains(searchTerm)) ||
                    (!string.IsNullOrWhiteSpace(r?.LastName) && r.LastName.ToLower().Contains(searchTerm)) ||
                    (!string.IsNullOrWhiteSpace(r?.PhoneNumber) && r.PhoneNumber.Contains(searchTerm)) ||
                    (!string.IsNullOrWhiteSpace(r?.Email) && r.Email.ToLower().Contains(searchTerm)) ||
                    (!string.IsNullOrWhiteSpace(r?.FirstName) && !string.IsNullOrWhiteSpace(r.LastName) &&
                        $"{r.FirstName} {r.LastName}".ToLower().Contains(searchTerm))
                )
                .ToList();

            GroupRecipientsByFirstNameFirstLetter(filtered);
            Task.Run(async () =>
            {
                if (filtered.Count == 0)
                    Favorites = new ObservableCollection<Recipient?>();
                var recipientsList = await _databaseService.GetFavoriteRecipientAsync();
                foreach (var item in filtered)
                {
                    if (item.IsFavorite)
                        Favorites = new ObservableCollection<Recipient?>(recipientsList.Where(x => x?.Id == item.Id));
                }
            });
        }
    }
}