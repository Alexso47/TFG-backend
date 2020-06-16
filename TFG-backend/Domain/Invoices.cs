using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Invoices
    {
        public int Id { get; set; }

        public DateTimeOffset InvoiceDate { get; set; }

        public float Price { get; set; }

        public string Currency { get; set;}

        public bool BuyerEU { get; set; }

        public string BuyerId { get; set; }

        public string BuyerName { get; set; }

        public string BuyerCountry { get; set; }

        public string BuyerCity { get; set; }

        public string BuyerAddress { get; set; }

        public string BuyerZipCode { get; set; }

        public List<Serials> Serials { get; set; }
    }
}
