using NellsPay.Send.Models.Notificatio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.Notification
{
    public class NotificatiosVM : BaseViewModel
    {
        #region Fields
        private string _Search { get; set; }
        private ObservableCollection<Grouping<string, NotificatiosModel>> _GroupNotificatios { get; set; } = new ObservableCollection<Grouping<string, NotificatiosModel>>();
        private List<NotificatiosModel> _Notificatios { get; set; } = new List<NotificatiosModel>();
        private ObservableCollection<NotificationCatigory> _NotificatiosCatigory { get; set; } = new ObservableCollection<NotificationCatigory>();
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
        public ObservableCollection<NotificationCatigory> NotificatiosCatigory
        {
            get { return _NotificatiosCatigory; }
            set
            {
                if (_NotificatiosCatigory != value)
                {
                    _NotificatiosCatigory = value;
                    OnPropertyChanged();

                }
            }
        }
        public ObservableCollection<Grouping<string, NotificatiosModel>> GroupNotificatios
        {
            get { return _GroupNotificatios; }
            set
            {
                if (_GroupNotificatios != value)
                {
                    _GroupNotificatios = value;
                    OnPropertyChanged();

                }
            }
        }
        public List<NotificatiosModel> Notificatios
        {
            get { return _Notificatios; }
            set
            {
                if (_Notificatios != value)
                {
                    _Notificatios = value;
                    GroupNotificatiosByDate();
                    OnPropertyChanged();

                }
            }
        }
        #endregion
        #region Extra
        private readonly INotificatiosService _Service;
        #endregion
        public NotificatiosVM(INotificatiosService serviceProvider)
        {
            _Service = serviceProvider;
            NotificatiosCatigory = new ObservableCollection<NotificationCatigory>()
            {
                new NotificationCatigory { CatigoryTitle = "All", CatigoryId = "1", IsActive = true },
                new NotificationCatigory { CatigoryTitle = "Sent", CatigoryId = "2" },
                new NotificationCatigory { CatigoryTitle = "Received", CatigoryId = "3" },
                new NotificationCatigory { CatigoryTitle = "Payment", CatigoryId = "4" },
                new NotificationCatigory { CatigoryTitle = "Catigory1", CatigoryId = "5" },

            };
            Task.Run(async () =>
            {
                await GetData("1");
            });
        }
        public async Task GetData(string CatigoryId)
        {
            Notificatios = new List<NotificatiosModel>();
            if (CatigoryId == "1")
            {
                Notificatios = await _Service.GetNotificatios();
            }
            else
            {
                Notificatios = await _Service.GetNotificatinsByCatigory(CatigoryId);
            }
           
      
        }
        private void GroupNotificatiosByDate()
        {


            if (Notificatios == null || !Notificatios.Any())
            {
                GroupNotificatios = new ObservableCollection<Grouping<string, NotificatiosModel>>();
                return;
            }

            var sorted = Notificatios
                .OrderByDescending(r => r.Date) 
                .GroupBy(r => r.Date.Date)
                .Select(g =>
                {
                    var items = g.Select((item, index) => {
                        item.IsLastItem = index == g.Count() - 1;
                        return item;
                    }).ToList();
                    return new Grouping<string, NotificatiosModel>(FormatDate(g.Key), items);
                });

            GroupNotificatios = new ObservableCollection<Grouping<string, NotificatiosModel>>(sorted);
            OnPropertyChanged(nameof(GroupNotificatios));
        }
        private string FormatDate(DateTime date)
        {
            if (date.Date == DateTime.Today)
                return "Today";
            if (date.Date == DateTime.Today.AddDays(-1))
                return "Yesterday";
            return date.ToString("MMMM dd, yyyy");
        }
        public ICommand BackCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("..");


        });
   
        public ICommand ExtraCommand => new Command(() =>
        {
            //
        });

        public ICommand SelectCatigoryCommand
        {
            get
            {
                return new Command((e) =>
                {

                    var item = (e as NotificationCatigory);


                   foreach (var catigory in NotificatiosCatigory)
                    {
                        if (catigory.CatigoryId == item.CatigoryId)
                        {
                            catigory.IsActive = true;
                        }
                        else
                        {
                            catigory.IsActive = false;
                        }
                    }
                    OnPropertyChanged(nameof(NotificatiosCatigory));
                    Task.Run(async () =>
                    {
                        await GetData(item.CatigoryId);
                    });

                });

            }

        }
    }
}
