using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.dto.Dto.Invoice
{
    public class InvoiceReferenceResponse
    {
        public string InvoiceNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset? InvoiceDate { get; set; }
    }
}
