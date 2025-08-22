using NellsPay.Send.Views.TransactionPages;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels
{
    public partial class TransactionViewModel : BaseViewModel
    {
        private readonly IToastService _toastService;
        private readonly ITransactionService _transactionService;

        private const int PageSize = 10;

        [ObservableProperty] private string search;
        [ObservableProperty] private bool isLoadingMore;
        [ObservableProperty] private bool isLoading;
        [ObservableProperty] private int itemsPerPage = PageSize;
        [ObservableProperty] private int currentIndex = 0;
        [ObservableProperty] private bool hasMoreData = true;

        [ObservableProperty] private ObservableCollection<Grouping<string, Transactions>> groupTransactions = new();
        public ObservableCollection<Transactions> Transactions { get; } = new();

        public TransactionViewModel(IToastService toastService, ITransactionService transactionService)
        {
            _toastService = toastService;
            _transactionService = transactionService;

            Task.Run(async () =>
            {
                IsLoading = true;
                await LoadNextBatchAsync();
            });
        }

        private async Task LoadNextBatchAsync()
        {
            await GetDataAsync(CurrentIndex, ItemsPerPage);
        }

        private async Task GetDataAsync(int offset, int limit)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Fetching transactions (offset={offset}, limit={limit})");

                var response = await _transactionService.GetTransactions(offset, limit);
                var data = response?.Transactions?.data;

                if (data != null && data.Any())
                {
                    var newItems = data.Select(d => new Transactions
                    {
                        Id = new Guid(d.Id),
                        TransactionNumber = d.TransactionNumber,
                        Status = d.Status,
                        StatusFlag = "",
                        Recipient = new Recipient
                        {
                            Initials = $"{d.FirstName?.FirstOrDefault()}{d.LastName?.FirstOrDefault()}".ToUpper(),
                            FirstName = d.RecipientFirstName,
                            LastName = d.RecipientLastName,
                            PhoneNumber = d.RecipientPhoneNumber,
                            Country = d.RecipientCountry,
                            PayOutAccount = d.PayOutAccount,
                        },
                        ReciverCurrency = d.RecipientCurrency,
                        SenderAmount = d.SenderAmount,
                        SenderCurrency = d.SenderCurrency,
                        SenderCountry = d.SenderCountry,
                        ReceiverAmount = d.ReceivedAmount,
                        TransactionFee = d.TransactionFee,
                        ExchangeRate = d.ExchangeRate,
                        Date = d.CreatedAt.ToString("yyyy-MM-dd"),
                        Time = d.CreatedAt.ToString("HH:mm:ss"),
                    }).ToList();

                    foreach (var item in newItems)
                        Transactions.Add(item);

                    CurrentIndex++;
                    HasMoreData = Transactions.Count < response?.Transactions.count;

                    GroupTransactionsByDate();
                }
                else
                {
                    HasMoreData = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Transaction fetch failed: {ex.Message}");
                _toastService.ShowToast("Failed to load transactions. Please try again later.");
            }
            finally
            {
                IsLoading = false;
                IsLoadingMore = false;
            }
        }
        private void GroupTransactionsByDate()
        {
            try
            {
                if (!Transactions.Any())
                    return;

                var grouped = Transactions
                    .Where(t => !string.IsNullOrWhiteSpace(t.Date) &&
                                DateTime.TryParseExact(t.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    .OrderByDescending(t => DateTime.ParseExact(t.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                    .GroupBy(t => DateTime.ParseExact(t.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date)
                    .Select(g => new Grouping<string, Transactions>(FormatDate(g.Key), g))
                    .ToList();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    foreach (var newGroup in grouped)
                    {
                        var existingGroup = GroupTransactions.FirstOrDefault(g => g.Key == newGroup.Key);

                        if (existingGroup == null)
                        {
                            GroupTransactions.Add(newGroup);
                        }
                        else
                        {
                            foreach (var transaction in newGroup)
                            {
                                if (!existingGroup.Contains(transaction))
                                    existingGroup.Add(transaction);
                            }
                        }
                    }

                    OnPropertyChanged(nameof(GroupTransactions));
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[GroupTransactionsByDate] Error: {ex.Message}");
            }
        }
        private static string FormatDate(DateTime date)
        {
            int day = date.Day;
            string suffix = GetDaySuffix(day);
            return $"{day}{suffix} {date:MMMM}";
        }

        private static string GetDaySuffix(int day)
        {
            return (day % 10 == 1 && day != 11) ? "st" :
                   (day % 10 == 2 && day != 12) ? "nd" :
                   (day % 10 == 3 && day != 13) ? "rd" : "th";
        }

        [RelayCommand]
        private async Task Selected(Transactions item)
        {
            var detail = new TransactionDetailModel
            {
                Id = item.Id,
                SenderFlag = item.Recipient.CountryFlag ?? "fl_ci.png",
                ReciverFlag = "fl_cm.png",
                RecipientName = item.Recipient.FullName,
                Image = item.Recipient.Image,
                Initials = item.Recipient.Initials,
                Date = item.Date,
                Time = item.Time,
                Amount = $"{item.SenderAmount} {item.SenderCurrency}",
                Transferamount = $"{item.SenderAmount} {item.SenderCurrency}",
                Totaltorecipient = $"{item.ReceiverAmount:N2} {item.ReciverCurrency}",
                TransactionID = item.TransactionNumber,
                Status = item.Status,
                Deliverymethod = "Mobile Money",
                Accountnumber = item.Recipient.PayOutAccount,
                Reasonoftransaction = "Gift",
                Additionalnote = "Money for Birthday gift",
                Transactionnumber = item.TransactionNumber,
                TransactionDate = DateTime.Parse(item.Date),
                SenderCountry = item.SenderCurrency,
                ReciverCurrency = item.ReciverCurrency,
                PayOutAccount = item.Recipient.PayOutAccount,
            };

            await Shell.Current.GoToAsync($"{nameof(TransactionDetailsPage)}?", new Dictionary<string, object>
            {
                ["TransactionDetails"] = detail
            });
        }

        [RelayCommand]
        private async Task LazyLoader()
        {
            if (!HasMoreData || IsLoadingMore)
                return;

            IsLoadingMore = true;
            await LoadNextBatchAsync();
        }
    }
}

