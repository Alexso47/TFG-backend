using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IFacilitiesRepository
    {
        Task<string> Add(Facilities facility);
    }
}
