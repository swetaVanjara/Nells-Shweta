using NellsPay.Send.Models.Notificatio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Services
{
    public class NotificatiosService : INotificatiosService
    {
        private List<NotificatiosModel> NotificatioList = new List<NotificatiosModel>();

        public NotificatiosService()
        {
            AllNotifications();
        }
        public Task<List<NotificatiosModel>> GetNotificatios()
        {
         
            return Task.FromResult(NotificatioList);
        }

        public Task<List<NotificatiosModel>> GetNotificatinsByCatigory(string CatigoryId)
        {
            return Task.FromResult(NotificatioList.Where(I=>I.NotificationCatigory == CatigoryId).ToList());
        }

        private void AllNotifications()
        {
            NotificatioList = new List<NotificatiosModel>()
            {
                new NotificatiosModel {
                    Image="usertest2.png",
                    Title = "You Get Cashback!",
                    NotificationCatigory = "2",
                    Description = "You get $10 cashback from payment",
                    Date = DateTime.Now
                },
                  new NotificatiosModel {
                      Image="usertest2.png",
                       NotificationCatigory = "2",
                    Title = "YNew Service is Available!",
                    Description = "You get $10 cashback from payment",
                    Date = DateTime.Now.AddDays(-1)
                },
                  new NotificatiosModel {
                      Image="usertest2.png",
                       NotificationCatigory = "2",
                    Title = "Apple Music Subscription Bill",
                    Description = "You get $10 cashback from payment",
                    Date = DateTime.Now.AddDays(-1)
                },
                  new NotificatiosModel {Image = "usertest2.png",
                    Title = "E-Wallet is Connected!",
                     NotificationCatigory = "3",
                    Description = "You get $10 cashback from payment",
                    Date = DateTime.Now.AddDays(-1)
                },
                  new NotificatiosModel {Image = "usertest2.png",
                    Title = "You Get Cashback!",
                    NotificationCatigory = "3",
                    Description = "You get $10 cashback from payment",
                    Date = DateTime.Now.AddDays(-2)
                },
                  new NotificatiosModel {Image = "usertest2.png",
                    Title = "You Get Cashback!2",
                     NotificationCatigory = "4",
                    Description = "You get $10 cashback from payment",
                    Date = DateTime.Now.AddDays(-2)
                },
                   new NotificatiosModel {Image = "usertest2.png",
                    Title = "You Get Cashback!3",
                     NotificationCatigory = "5",
                    Description = "You get $10 cashback from payment",
                    Date = DateTime.Now.AddDays(-2)
                },
            };
        }
    }
}
