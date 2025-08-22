using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models
{
    public class CountryCodes
    {
        public string Name { get; set; } = default!;
        public string ISO { get; set; } = default!;
        public string DialCode { get; set; } = default!;
        public string Flag { get; set; } = default!;
    }
}
