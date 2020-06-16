using System;
using System.Collections.Generic;
using System.Text;

namespace API.dto.Dto.EconomicOperator
{
    public class EOResponse : GenericResponse
    {
        public EOReferenceResponse Reference { get; set; }
    }
}
