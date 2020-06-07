using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class InvoiceQueryString
    {
        public DateTimeOffset? CreationDateFrom { get; set; }

        public DateTimeOffset? CreationDateTo { get; set; }

        public int? Id { get; set; }

        public float? Price { get; set; }

        public string Currency { get; set; }

        public bool? BuyerEU { get; set; }

        public string BuyerID { get; set; }
    }
}
