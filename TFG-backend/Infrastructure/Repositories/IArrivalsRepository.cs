using Domain;
using Domain.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IArrivalsRepository
    {
        Task<ArrivalsDB> Add(ArrivalsDB arrival, List<SerialsDB> serials);
    }
}
