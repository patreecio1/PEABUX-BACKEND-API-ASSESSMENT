using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Models
{
    public class EmailRequestModel
    {
        public string SenderDisplayName { get; set; }
        public string Subject { get; set; }
        public string[] Attachement { get; set; } = Array.Empty<string>();
        public Dictionary<string, string> DestinationEmail { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Bcc { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Cc { get; set; } = new Dictionary<string, string>();
        public string Body { get; set; }


    }


}


