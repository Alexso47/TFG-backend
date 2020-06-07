using Application.Models;
using Domain;
using Domain.Filters;
using Infrastructure.Services;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Invoice
{
    public class InvoiceQueryHandler : IRequestHandler<InvoiceQuery, PaginatedList<InvoiceResult>>
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceQueryHandler(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<PaginatedList<InvoiceResult>> Handle(InvoiceQuery request, CancellationToken cancellationToken)
        {
            var filter = GetFilter(request);
            var invoicees = await _invoiceService.GetInvoices(filter);
            int total = await _invoiceService.GetTotalInvoices(filter);
            PaginatedList<InvoiceResult> result = CreateResultResponse(request, invoicees);
            result.Total = total;
            return result;
        }

        private InvoiceFilter GetFilter(InvoiceQuery request)
        {
            return new InvoiceFilter()
            {
                InvoiceDateFrom = request.DateFrom,
                InvoiceDateTo = request.DateTo,
                Id = request.Id.Value,
                BuyerEU = request.BuyerEU.Value,
                BuyerID = request.BuyerID,
                Currency = request.Currency,
                Price = request.Price.Value
            };
        }

        private PaginatedList<InvoiceResult> CreateResultResponse(InvoiceQuery request, List<Invoices> result)
        {
            var invoicesResult = new List<InvoiceResult>();
            var filteredList = result;

            foreach (var invoice in filteredList)
            {
                var invoiceResult = new InvoiceResult()
                {
                    Id = invoice.Id,
                    InvoiceDate = invoice.InvoiceDate.Date,
                    Price = invoice.Price,
                    Currency = invoice.Currency,
                    BuyerEU = invoice.BuyerEU,
                    BuyerID = invoice.BuyerId,
                    BuyerName = invoice.BuyerName,
                    BuyerCountry = invoice.BuyerCountry,
                    BuyerCity = invoice.BuyerCity,
                    BuyerAddress = invoice.BuyerAddress,
                    BuyerZipCode = invoice.BuyerZipCode,
                    Serials = invoice.Serials,
                    SerialsJson = JsonConvert.SerializeObject(invoice.Serials),
                };

                invoicesResult.Add(invoiceResult);
            }

            return new PaginatedList<InvoiceResult>(invoicesResult);
        }
    }
}
