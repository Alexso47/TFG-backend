using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Filters
{
    public class ArrivalFilter
    {
        public int Id { get; set; }

        public DateTimeOffset? ArrivalDateFrom { get; set; }

        public DateTimeOffset? ArrivalDateTo { get; set; }

        public string FID { get; set; }
    }
}
