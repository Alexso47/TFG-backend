using System;
using System.Collections.Generic;
using System.Text;

namespace API.dto.Dto.Invoice
{
    public class InvoiceReferenceRequest
    {
        public string Id { get; set; }

        public DateTimeOffset InvoiceDate { get; set; }

        public float Price { get; set; }

        public string Currency { get; set; }

        public byte? BuyerEU { get; set; }

        public string BuyerID { get; set; }

        public string BuyerName { get; set; }

        public string BuyerCountry { get; set; }

        public string BuyerAddress { get; set; }

        public string BuyerCity { get; set; }

        public string BuyerZipCode { get; set; }
    }
}
