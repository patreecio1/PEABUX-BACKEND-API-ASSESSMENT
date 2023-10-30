using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthInsuranceERP.API.RequestModels
{
    public class UnitVM : Model
    {
        public long Id { get; set; }
        //    public long DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
   
   
}
