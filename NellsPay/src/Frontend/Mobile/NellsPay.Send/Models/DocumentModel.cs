using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models
{
    public class DocumentModel
    {
        public string Id { get; set; } = string.Empty;
        public string DocumentName { get; set; } = string.Empty; 
        public string DocumentDescription { get; set; } = string.Empty;
        public string DocumentIcon { get; set; } = string.Empty;
    }
}
