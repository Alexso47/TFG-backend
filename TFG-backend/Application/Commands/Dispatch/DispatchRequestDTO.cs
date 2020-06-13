using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Dispatch
{
    public class DispatchRequestDTO
    {
        public string FID { get; set; }

        public DateTimeOffset DispatchDate { get; set; }

        public byte DestinationEU { get; set; }

        public string DestinationFID { get; set; }

        public string DestinationName { get; set; }

        public string DestinationCountry { get; set; }

        public string DestinationAddress { get; set; }

        public string DestinationCity { get; set; }

        public string DestinationZipCode { get; set; }

        public string TransportMode { get; set; }

        public string Vehicle { get; set; }

        public List<string> Serials { get; set; }
    }
}
