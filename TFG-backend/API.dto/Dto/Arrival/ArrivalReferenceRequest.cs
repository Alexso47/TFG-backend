using System;
using System.Collections.Generic;
using System.Text;

namespace API.dto.Dto.Arrival
{
    public class ArrivalReferenceRequest
    {
        public string FID { get; set; }

        public DateTimeOffset ArrivaleDate { get; set; }

        public string Comments { get; set; }

    }
}
