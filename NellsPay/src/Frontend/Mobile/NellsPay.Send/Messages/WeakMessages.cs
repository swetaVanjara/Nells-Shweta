using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Messages
{
    public class WeakMessages : ValueChangedMessage<string>
    {
        public WeakMessages(string value) : base(value)
        {
        }
        public class ChangeCurrencyMessage : ValueChangedMessage<(Currency, string)>
        {
            
            public ChangeCurrencyMessage((Currency, string) value) : base(value)
            {
            }
        }
        public class EditRecipientMessage : ValueChangedMessage<(Recipient, string)>
        {

            public EditRecipientMessage((Recipient, string) value) : base(value)
            {
            }
        }
    }
}
