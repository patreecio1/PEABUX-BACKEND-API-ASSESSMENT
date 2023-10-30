using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Models
{
    public class Page<T>
    {
        public Page()
        {
        }

        public Page(T[] t, long pageCount, int pageNumber, int pageSize)
        {
            this.Data = t;
            this.PageCount = pageCount;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public T[] Data { get; set; }
        public long PageCount { get; set; }
    }
}
