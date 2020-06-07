using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Invoice
{
    public class SubmitInvoiceCommand : BaseCommand , IRequest<string>
    {
        public SubmitInvoiceCommand(InvoiceRequestDTO dto, string requestSerialized, string requestObjectName) : base(requestObjectName)
        {
            Request = dto;
            RequestSerialized = requestSerialized;
        }

        public InvoiceRequestDTO Request { get; set; }
    }
}
