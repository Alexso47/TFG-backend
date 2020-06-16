using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class DispatchQueryString
    {
        public DateTimeOffset? CreationDateFrom { get; set; }

        public DateTimeOffset? CreationDateTo { get; set; }

        public int? Id { get; set; }

        public string FID { get; set; }

        public bool? DestinationEU { get; set; }

        public string DestinationFID { get; set; }

        public string TransportMode { get; set; }

        public string Vehicle { get; set; }
    }
}
