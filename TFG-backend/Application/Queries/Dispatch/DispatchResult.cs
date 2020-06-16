using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Dispatch
{
    public class DispatchResult
    {
        public int Id { get; set; }

        public string FID { get; set; }

        public DateTime DispatchDate { get; set; }

        public bool DestinationEU { get; set; }

        public string DestinationFID { get; set; }

        public string DestinationCountry { get; set; }
        
        public string DestinationCity { get; set; }
       
        public string DestinationAddress { get; set; }
        
        public string DestinationZipCode { get; set; }

        public string TransportMode { get; set; }

        public string Vehicle { get; set; }

        public List<Serials> Serials { get; set; }

        public string SerialsJson { get; set; }
    }
}
