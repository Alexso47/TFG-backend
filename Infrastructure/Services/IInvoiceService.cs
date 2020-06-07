using Domain;
using Domain.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IInvoiceService
    {
        Task<List<Invoices>> GetInvoices(InvoiceFilter filter);

        Task<int> GetTotalInvoices(InvoiceFilter filter);

        Task<Invoices> GetInvoiceById(int id);

        Task<int> GetLastIdInvoice();

        Task<int> GetLastIdInvoiceSerials();

        Task<int> UpdateInvoiceSerialsTable(InvoiceSerials item);
    }
}
