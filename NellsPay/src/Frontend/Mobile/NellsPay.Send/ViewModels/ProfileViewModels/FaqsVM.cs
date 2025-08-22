using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.ProfileViewModels
{
    public partial class FaqsVM : BaseViewModel
    {
        #region Fields
        private ObservableCollection<FaqsModel> _FaqsList { get; set; } = new ObservableCollection<FaqsModel>();
        #endregion
        #region Property
        public ObservableCollection<FaqsModel> FaqsList
        {
            get { return _FaqsList; }
            set
            {
                if (_FaqsList != value)
                {
                    _FaqsList = value;
                    OnPropertyChanged();

                }
            }
        }
        #endregion

        public FaqsVM()
        {
            GetData();
        }

        private void GetData()
        {
            FaqsList = new ObservableCollection<FaqsModel>() 
            
            {
                new FaqsModel(){Title = "What is NellsPay?", Description = "NellsPay is a payment platform that allows you to send money to your loved ones in Nigeria. You can send money to any bank account in Nigeria, and the money will be available in the recipient's account within minutes."},
                new FaqsModel(){Title = "How do I send money with NellsPay?", Description = "To send money with NellsPay, you need to create an account on our platform. Once you have created an account, you can add your recipient's details and send money to them. You can pay for your transfer using your debit card or bank account."},
                new FaqsModel(){Title = "How long does it take for the money to reach the recipient?", Description = "The money will be available in the recipient's account within minutes of the transfer being completed. However, in some cases, it may take up to 24 hours for the money to be credited to the recipient's account."},
                new FaqsModel(){Title = "How much does it cost to send money with NellsPay?", Description = "The cost of sending money with NellsPay depends on the amount you are sending and the payment method you choose. You can use our fee calculator to estimate the cost of your transfer."},
                new FaqsModel(){Title = "Is it safe to send money with NellsPay?", Description = "Yes, it is safe to send money with NellsPay. We use industry-standard security measures to protect your personal and financial information. Additionally, all transactions are encrypted to ensure that your data is secure."},
                new FaqsModel(){Title = "What payment methods can I use to send money with NellsPay?", Description = "You can pay for your transfer using your debit card or bank account. We accept all major debit cards, including Visa, MasterCard, and American Express."}

            };
        }
        public ICommand BackCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("..");
        });
        public ICommand ExpandCommand
        {
            get
            {
                return new Command((e) =>
                {

                    var item = (e as FaqsModel);
                    if (item != null)
                    {
                        item.Expanded = !item.Expanded;
                    }

                });

            }

        }

    }
}
