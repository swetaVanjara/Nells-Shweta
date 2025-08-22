using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models.Notificatio
{
    public class NotificationCatigory : BaseViewModel
    {
        public string CatigoryId { get; set; } = string.Empty;
        public string CatigoryTitle { get; set; } = string.Empty;
        private bool _IsActive { get; set; } = false;
        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged();

                }
            }
        }
    }
}
