using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Data.Entities
{
    public class Entity<T>
    {
        public T Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; } = DateTimeOffset.Now;
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
