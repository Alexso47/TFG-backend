using Domain;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EORepository : IEORepository
    {

        private readonly DBContext _context;
        private IEconomicOperatorService _economicOperatorService;

        public EORepository(DBContext context, IEconomicOperatorService economicOperatorService)
        {
            _context = context;
            _economicOperatorService = economicOperatorService;
        }

        public async Task<string> Update(EconomicOperators economicOperators)
        {
            string deleted;
            try
            {
                deleted = await _economicOperatorService.DeleteEO(economicOperators.Id);
            }
            catch
            {
                throw new Exception("No se puede modificar el EO");
            }

            if (deleted != "")
            {
                var result = await _context.EconomicOperators.AddAsync(economicOperators);
                await _context.SaveChangesAsync();
                return result.Entity.Id;
            }
            else
            {
                throw new Exception("Error actualizando el EO");
            }
        }

        public async Task<string> Add(EconomicOperators economicOperators)
        {
            var exist = await _economicOperatorService.GetEOById(economicOperators.Id);
            if (exist == null)
            {
                try
                {
                    var result = await _context.EconomicOperators.AddAsync(economicOperators);
                    await _context.SaveChangesAsync();
                    return result.Entity.Id;
                }
                catch
                {
                    throw new Exception("Error creando el EO");
                }
                
            }
            else
            {
                throw new Exception("Ya existe este EO");
            }
        }

    }
}
