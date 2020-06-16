using Domain;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DispatchesRepository : IDispatchesRepository
    {

        private readonly DBContext _context;
        private readonly IDispatchService _dispatchesService;
        private readonly ISerialService _serialService;


        public DispatchesRepository(DBContext context, IDispatchService dispatchesService, ISerialService serialService)
        {
            _context = context;
            _dispatchesService = dispatchesService;
            _serialService = serialService;
        }

        public async Task<DispatchesDB> Add(DispatchesDB dispatch, List<SerialsDB> serials)
        {
           
            EntityEntry<DispatchesDB> result;
            try
            {
                result = await _context.Dispatches.AddAsync(dispatch);
            }
            catch 
            {
                throw new Exception("Error al registrar el envio");
            }
                
            int added;
            try
            {
                added = AddSerials(serials, dispatch.Id).Result;
            }
            catch
            {
                throw new Exception("Error insertando los seriales");
            }

            if (added == serials.Count)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return result.Entity;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("Algunos seriales no se han insertado, se ha cancelado la operacion");
            }
        }

        private async Task<int> AddSerials(List<SerialsDB> serials, int id)
        {
            int adds = 0;
            int idSerial = _dispatchesService.GetLastIdDispatchSerials().Result;
            idSerial++;
            foreach (var s in serials)
            {
                var existingSerial = _serialService.GetSerialBySerial(s.Serial);
                var aux = existingSerial.Result != null ? existingSerial.Result.Id : 0;
                if (aux != 0)
                {
                    var newItem = new DispatchSerials
                    {
                        Id = idSerial,
                        Serial = s.Serial.ToString(),
                        DispatchID = id
                    };
                    idSerial++;

                    var res = await _dispatchesService.UpdateDispatchSerialsTable(newItem);
                    adds += res;
                }
            }
            return adds;
        }
    }
}
