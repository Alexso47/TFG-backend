using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ArrivalQueryString
    {
        public DateTimeOffset? CreationDateFrom { get; set; }

        public DateTimeOffset? CreationDateTo { get; set; }

        public int? Id { get; set; }

        public string FID { get; set; }
    }
}
