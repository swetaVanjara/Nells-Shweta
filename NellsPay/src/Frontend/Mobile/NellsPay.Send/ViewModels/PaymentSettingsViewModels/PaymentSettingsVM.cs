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
    public class PaymentSettingsVM : BaseViewModel
    {
        #region Fields
        private ObservableCollection<CardsModel>  _Cards { get; set; } = new ObservableCollection<CardsModel>();
        #endregion
        #region Property
        public ObservableCollection<CardsModel> Cards
        {
            get { return _Cards; }
            set
            {
                if (_Cards != value)
                {
                    _Cards = value;
                    OnPropertyChanged();

                }
            }
        }
     
        #endregion
        #region Extra

        #endregion
        public PaymentSettingsVM()
        {
            GetData();
        }


        #region Methods
        private void GetData() // this for testing
        {
            Cards = new ObservableCollection<CardsModel>
            {
                new CardsModel
                {
                    CardNumber = "1234 5678 9012 3456",
                    CardHolderName = "John Doe",
                    ExpiredYear = "2027",
                    ExpiredMonth = "12",
                },
                new CardsModel
                {
                    CardNumber = "1234 5678 9012 3456",
                    CardHolderName = "John Doe",
                    ExpiredYear = "2027",
                    ExpiredMonth = "12",
                },

            };
        }


        #endregion

        #region Command

        public ICommand AddNewCardCommand => new Command( async () =>
        {
            //  await Shell.Current.GoToAsync($"{nameof(AddManuallyPage)}?");
           // await Shell.Current.GoToAsync("//ProfilePage/AddManuallyPage");
          
         Shell.Current.CurrentPage.ShowPopup(new AddCardOptionPopUp());

        });
        public ICommand BackCommand => new Command(async() =>
        {
            await Shell.Current.GoToAsync("..");
        });

        public ICommand DeleteCommand
        {
            get
            {
                return new Command( (e) =>
                {

                    var item = (e as CardsModel);
                    if (item != null)
                    {
                        Cards.Remove(item);
                    }

                });

            }

        }
        #endregion
    }
}
