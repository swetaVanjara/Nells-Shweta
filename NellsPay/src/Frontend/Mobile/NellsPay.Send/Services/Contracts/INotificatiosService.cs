using NellsPay.Send.Models.Notificatio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Services.Contracts
{
    public interface INotificatiosService
    {
        Task<List<NotificatiosModel>> GetNotificatios();
        Task<List<NotificatiosModel>> GetNotificatinsByCatigory(string CatigoryId);
    }
}
