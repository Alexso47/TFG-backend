using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IEORepository
    {
        Task<string> Update(EconomicOperators economicOperators);

        Task<string> Add(EconomicOperators economicOperators);
    }
}
