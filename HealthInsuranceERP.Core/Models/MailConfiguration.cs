using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Models
{
    public class MailConfiguration
    {
        public string MailFromName { get; set; }
        public string MailFrom { get; set; }
        public string SMTPServer { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public int SMTPPort { get; set; }
    }
}
