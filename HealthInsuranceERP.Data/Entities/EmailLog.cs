
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Data.Entities
{
    public  class EmailLog:Entity<long>
    {
        public string Subject { get; set; }
        public string MailBody { get; set; }
        public string Recipient { get; set; }

        public DateTime Date { get; set; }
        public string Status { get; set; }

        public string SMTPServer { get; set; }
        public string SMTPPort { get; set; }

        public string MailFrom { get; set; }
        public string MailFromName { get; set; }


        
    }
}
