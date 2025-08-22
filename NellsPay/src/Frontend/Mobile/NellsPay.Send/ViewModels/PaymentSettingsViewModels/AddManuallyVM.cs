using CommunityToolkit.Maui.Views;
using NellsPay.Send.Views.PaymentSettingsPages;
using NellsPay.Send.Views.PopUpPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.PaymentSettingsViewModels
{
    public class AddManuallyVM : BaseViewModel
    {
        #region Fields
        private CardsModel _Card { get; set; } = new CardsModel();
        private bool _EnabelAddCard { get; set; } = true; // change that depend on logic
        private string _selectedMonth;
        private string _selectedYear;
     
        #endregion
        #region Property
        public CardsModel Card
        {
            get { return _Card; }
            set
            {
                if (_Card != value)
                {
                    _Card = value;
                    OnPropertyChanged();

                }
            }
        }
        public bool EnabelAddCard
        {
            get { return _EnabelAddCard; }
            set
            {
                if (_EnabelAddCard != value)
                {
                    _EnabelAddCard = value;
                    OnPropertyChanged();

                }
            }
        }

        public ObservableCollection<string> Months { get; set; }
        public ObservableCollection<string> Years { get; set; }
        public string SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                if (_selectedMonth != value)
                {
                    _selectedMonth = value;
                    OnPropertyChanged(nameof(SelectedMonth));
                }
            }
        }

        public string SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                }
            }
        }

        #endregion
        #region Extra

        #endregion
        public AddManuallyVM()
        {
            // Populate Months (01 to 12)
            Months = new ObservableCollection<string>();
            for (int i = 1; i <= 12; i++)
            {
                Months.Add(i.ToString("D2")); // Formats as "01", "02", ..., "12"
            }

            // Populate Years (Current Year to +10 Years)
            Years = new ObservableCollection<string>();
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i <= 10; i++)
            {
                Years.Add((currentYear + i).ToString());
            }
        }


        #region Methods


        #endregion

        #region Command

        public ICommand AddCardCommand => new Command(() =>
        {
            Card.ExpiredMonth = SelectedMonth;
            Card.ExpiredYear = SelectedYear;
            string last4 = Card.CardNumber?.Length >= 4
    ? Card.CardNumber.Substring(Card.CardNumber.Length - 4)
    : Card.CardNumber;
            Card.CardNumber = "**** **** **** " + last4;
            Shell.Current.CurrentPage.ShowPopup(new AddCardSuccessfullyPopUp(Card));
        });
        public ICommand BackCommand => new Command(async() =>
        {
            await Shell.Current.GoToAsync("//ProfilePage/PaymentSettingsPage");
        });


        #endregion
    }
}
