using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IEconomicOperatorService
    {
        Task<List<string>> GetEOIDS();
        
        Task<List<EconomicOperators>> GetEOID(string EOID);

        Task<string>DeleteEO(string id);

        Task<string> GetEOById(string id);
        
        Task<EconomicOperators> GetEO(string id);
    }
}
