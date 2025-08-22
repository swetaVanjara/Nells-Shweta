using CommunityToolkit.Mvvm.Messaging;
using NellsPay.Send.Models.MoneyTransferFlowModels;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.MoneyTransferFlowViewModels
{
    [QueryProperty(nameof(ReviewTransiction), "ReviewTransiction")]
    public class ChooserecipientsVM : BaseViewModel
    {



        #region Fields
        private ReviewTransictionModel _ReviewTransiction { get; set; } = new ReviewTransictionModel();
        private string _Search { get; set; }
        private ObservableCollection<Recipient> _RecipientList { get; set; } = new ObservableCollection<Recipient>();
        private ObservableCollection<Recipient> _Favorites { get; set; } = new ObservableCollection<Recipient>();
        #endregion
        #region Property
        public string Search
        {
            get { return _Search; }
            set
            {
                if (_Search != value)
                {
                    _Search = value;
                    OnPropertyChanged();

                }
            }
        }

        public ReviewTransictionModel ReviewTransiction
        {
            get { return _ReviewTransiction; }
            set
            {
                if (_ReviewTransiction != value)
                {
                    _ReviewTransiction = value;
                    OnPropertyChanged();

                }
            }
        }
        public ObservableCollection<Recipient> RecipientList
        {
            get { return _RecipientList; }
            set
            {
                if (_RecipientList != value)
                {
                    _RecipientList = value;
                    OnPropertyChanged();

                }
            }
        }
        public ObservableCollection<Recipient> Favorites
        {
            get { return _Favorites; }
            set
            {
                if (_Favorites != value)
                {
                    _Favorites = value;
                    OnPropertyChanged();

                }
            }
        }
        #endregion
        #region Extra
        private readonly IRecipientService _Service;
        #endregion


        public ChooserecipientsVM(IRecipientService serviceProvider)
        {
            _Service = serviceProvider;
            RecipientList = new ObservableCollection<Recipient>();
            Task.Run(async () =>
            {
                await GetData();
            });

        }

        #region Methods

        public async Task GetData()
        {
            // RecipientList = new ObservableCollection<Recipient>(await _Service.GetRecipientsAsync());
            Favorites = new ObservableCollection<Recipient>(RecipientList.Where(x => x.IsFavorite).ToList());
        }
        #endregion

        #region Command

        public ICommand SearchCommand => new Command(() =>
        {

        });

    
        public ICommand BackCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("..");
        });

        public ICommand ChooseRecipientCommand
        {
            get
            {
                return new Command(async (e) =>
                {

                    var item = (e as Recipient);
                    if (item != null)
                    {
                        ReviewTransiction.Recipient = item;
                        await Shell.Current.GoToAsync($"{nameof(ReviewTransictionPage)}?",
                 new Dictionary<string, object>
                 {
                     ["ReviewTransiction"] = ReviewTransiction,
                 });
                    }
                });
            }
        }
        public ICommand ChooseFavoritesRecipientCommand
        {
            get
            {
                return new Command((e) =>
                {

                    var item = (e as Recipient);
                    if (item != null)
                    {
                        // call the method to update the IsFavorite property
                        item.IsFavorite = !item.IsFavorite;
                        RecipientList.Where(x => x.Id == item.Id).FirstOrDefault().IsFavorite = item.IsFavorite;
                        Favorites = new ObservableCollection<Recipient>(RecipientList.Where(x => x.IsFavorite).ToList());
                    }
                });
            }
        }
        #endregion

    }
}
