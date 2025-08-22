
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models
{
    public class ProcesseModel : BaseViewModel
    {
        private string _ProccessTitle { get; set; } = default!;
        public string ProccessTitle
        {
            get { return _ProccessTitle; }
            set
            {
                if (_ProccessTitle != value)
                {
                    _ProccessTitle = value;
                    OnPropertyChanged();

                }
            }
        }
        private int _isProccessing { get; set; } = 0; // 0 not showing, 1 showing + Procces, 2 done
        public int isProccessing
        {
            get { return _isProccessing; }
            set
            {
                if (_isProccessing != value)
                {
                    _isProccessing = value;
                    OnPropertyChanged();

                }
            }
        }
    }
}
