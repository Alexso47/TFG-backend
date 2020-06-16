using System;
using System.Collections.Generic;
using System.Text;

namespace API.dto.Dto.Arrival
{
    public class ArrivalResponse : GenericResponse
    {
        public ArrivalReferenceResponse Reference { get; set; }
    }
}
