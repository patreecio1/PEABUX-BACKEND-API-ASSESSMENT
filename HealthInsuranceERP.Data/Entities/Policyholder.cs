using HealthInsuranceERP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Data.Entities
{
    public class Policyholder : Entity<int>
    {
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PolicyNumber { get; set; }
    }
}
