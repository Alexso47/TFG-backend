using Domain;
using Domain.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IDispatchService
    {
        Task<List<Dispatches>> GetDispatches(DispatchFilter filter);

        Task<int> GetTotalDispatches(DispatchFilter filter);
    }
}
