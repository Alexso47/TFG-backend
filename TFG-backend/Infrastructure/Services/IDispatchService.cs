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

        Task<Dispatches> GetDispatchById(int id);

        Task<int> GetLastIdDispatch();

        Task<int> GetLastIdDispatchSerials();

        Task<int> UpdateDispatchSerialsTable(DispatchSerials item);
    }
}
