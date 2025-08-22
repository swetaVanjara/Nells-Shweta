using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models.MoneyTransferFlowModels
{
    public class PaymentMethod : BaseViewModel
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Icon { get; set; } = default!;

        private bool _Selected { get; set; } = false;

        public bool Selected
        {
            get { return _Selected; }
            set
            {
                if (_Selected != value)
                {
                    _Selected = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
