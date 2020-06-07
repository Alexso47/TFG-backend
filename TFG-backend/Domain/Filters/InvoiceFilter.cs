using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Filters
{
    public class InvoiceFilter
    {
        public int Id { get; set; }

        public DateTimeOffset? InvoiceDateFrom { get; set; }

        public DateTimeOffset? InvoiceDateTo { get; set; }

        public bool BuyerEU { get; set; }

        public string BuyerID { get; set; }
        
        public float Price { get; set; }

        public string Currency { get; set; }
    }
}
