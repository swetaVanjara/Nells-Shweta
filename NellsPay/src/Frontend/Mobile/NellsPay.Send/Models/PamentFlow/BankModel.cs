using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models.PamentFlow
{
    public class BankModel
    {
        public string Id { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
