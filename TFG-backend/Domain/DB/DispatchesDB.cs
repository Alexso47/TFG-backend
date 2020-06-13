using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DispatchesDB
    {
        public int Id { get; set; }

        public string FID { get; set; }

        public DateTime DispatchDate { get; set; }

        public bool DestinationEU { get; set; }

        public string DestinationFID { get; set; }

        public string TransportMode { get; set; }

        public string Vehicle { get; set; }

    }
}
