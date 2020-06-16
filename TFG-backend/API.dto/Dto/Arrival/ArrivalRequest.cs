using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.dto.Dto.Arrival
{
    public class ArrivalRequest
    {
        public RequestHeader RequestHeader { get; set; }

        //public ArrivalReferenceRequest Reference { get; set; }

        public string FID { get; set; }

        public DateTimeOffset ArrivalDate { get; set; }

        public string Comments { get; set; }

        public List<string> Serials { get; set; }

    }
}
