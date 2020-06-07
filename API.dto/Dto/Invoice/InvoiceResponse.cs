using System;
using System.Collections.Generic;
using System.Text;

namespace API.dto.Dto.Invoice
{
    public class InvoiceResponse : GenericResponse
    {
        public InvoiceReferenceResponse Reference { get; set; }
    }
}
