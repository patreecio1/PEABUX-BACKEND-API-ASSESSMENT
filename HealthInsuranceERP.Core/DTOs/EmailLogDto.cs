
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Dtos
{
    public  class EmailLogDto : BaseModel<long>
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
        public string StatusMessage { get; set; }
    }
}
