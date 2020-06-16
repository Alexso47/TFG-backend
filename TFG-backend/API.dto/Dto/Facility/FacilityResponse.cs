using System;
using System.Collections.Generic;
using System.Text;

namespace API.dto.Dto.Facility
{
    public class FacilityResponse : GenericResponse
    {
        public FacilityReferenceResponse Reference { get; set; }
    }
}
