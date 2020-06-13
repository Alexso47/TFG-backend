using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.dto.Dto.Arrival
{
    public class ArrivalReferenceResponse
    {
        public string ArrivalNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset? ArrivalDate { get; set; }
    }
}
