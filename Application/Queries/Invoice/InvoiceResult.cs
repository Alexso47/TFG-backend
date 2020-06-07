using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Invoice
{
    public class InvoiceResult
    {
        public int Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public float Price { get; set; }

        public string Currency { get; set; }

        public bool BuyerEU { get; set; }

        public string BuyerID { get; set; }

        public string BuyerName { get; set; }

        public string BuyerCountry { get; set; }
        
        public string BuyerCity { get; set; }
       
        public string BuyerAddress { get; set; }
        
        public string BuyerZipCode { get; set; }

        public List<Serials> Serials { get; set; }

        public string SerialsJson { get; set; }
    }
}
