using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class InvoicesDB
    {
        public int Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public double Price { get; set; }

        public string Currency { get; set;}

        public bool BuyerEU { get; set; }

        public string BuyerId { get; set; }
    }
}
