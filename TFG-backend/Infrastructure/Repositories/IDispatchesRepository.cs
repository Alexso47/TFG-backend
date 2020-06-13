using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IDispatchesRepository
    {
        Task<DispatchesDB> Add(DispatchesDB dispatch, List<SerialsDB> serials);
    }
}
