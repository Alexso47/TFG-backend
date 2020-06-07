using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Filters
{
    public class DispatchFilter
    {
        public int Id { get; set; }

        public string FID { get; set; }

        public DateTimeOffset? DispatchDateFrom { get; set; }

        public DateTimeOffset? DispatchDateTo { get; set; }

        public bool? DestinationEU { get; set; }

        public string DestinationFID { get; set; }

        public string TransportMode { get; set; }

        public string Vehicle { get; set; }
    }
}
