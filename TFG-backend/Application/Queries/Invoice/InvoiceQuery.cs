using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Invoice
{
    public class InvoiceQuery : IRequest<PaginatedList<InvoiceResult>>
    {
        public InvoiceQuery(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int id, float price, string currency, string buyerId, bool? buyerEU)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
            Id = id;
            Price = price;
            Currency = currency;
            BuyerID = buyerId;
            BuyerEU = buyerEU;
        }

        public DateTimeOffset? DateFrom { get; set; }

        public DateTimeOffset? DateTo { get; set; }

        public int? Id { get; set; }

        public float? Price { get; set; }

        public string Currency { get; set; }

        public bool? BuyerEU { get; set; }

        public string BuyerID { get; set; }
    }
}