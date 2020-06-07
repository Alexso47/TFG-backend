using Domain;
using Domain.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IArrivalService
    {
        Task<List<Arrivals>> GetArrivals(ArrivalFilter filter);

        Task<int> GetTotalArrivals(ArrivalFilter filter);
    }
}
