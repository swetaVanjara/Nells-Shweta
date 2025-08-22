using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models
{
    public class FaqsModel : BaseViewModel
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;

        private bool _Expanded { get; set; } = false;
        public bool Expanded
        {
            get { return _Expanded; }
            set
            {
                if (_Expanded != value)
                {
                    _Expanded = value;
                    OnPropertyChanged();

                }
            }
        }
    }
}
