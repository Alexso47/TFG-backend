using System;
using System.Collections.Generic;
using System.Text;

namespace API.dto.Dto.Dispatch
{
    public class DispatchResponse : GenericResponse
    {
        public DispatchReferenceResponse Reference { get; set; }
    }
}
