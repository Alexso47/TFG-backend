using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.dto.Dto.Dispatch
{
    public class DispatchReferenceResponse
    {
        public string DispatchNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset? DispatchDate { get; set; }
    }
}
