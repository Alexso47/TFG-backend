using System;
using System.Collections.Generic;
using System.Text;

namespace API.dto.Dto.Dispatch
{
    public class DispatchReferenceRequest
    {
        public string FID { get; set; }

        public DateTimeOffset DispatcheDate { get; set; }

        public byte DestinationEU { get; set; }

        public string DestinationFID { get; set; }

        public string DestinationName { get; set; }

        public string DestinationCountry { get; set; }

        public string DestinationAddress { get; set; }

        public string DestinationCity { get; set; }

        public string DestinationZipCode { get; set; }

        public string TransportMode { get; set; }

        public string Vehicle { get; set; }
    }
}
