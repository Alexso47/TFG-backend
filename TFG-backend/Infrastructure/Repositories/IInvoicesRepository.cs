using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IInvoicesRepository
    {
        Task<InvoicesDB> Add(InvoicesDB invoice, List<SerialsDB> serials);
    }
}
