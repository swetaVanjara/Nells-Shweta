using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Services.Contracts
{
    public class Grouping<TKey, TItem> : ObservableCollection<TItem>, IGrouping<TKey, TItem>, INotifyPropertyChanged
    {
        public TKey Key { get; private set; }


        public Grouping(TKey key, IEnumerable<TItem> items)
        {
            Key = key;
            foreach (var item in items)
                this.Add(item);
        }

    }
}
