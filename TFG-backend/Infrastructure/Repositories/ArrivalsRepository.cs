using Domain;
using Domain.DB;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ArrivalsRepository : IArrivalsRepository
    {

        private readonly DBContext _context;
        private readonly IArrivalService _arrivalsService;
        private readonly ISerialService _serialService;


        public ArrivalsRepository(DBContext context, IArrivalService arrivalsService, ISerialService serialService)
        {
            _context = context;
            _arrivalsService = arrivalsService;
            _serialService = serialService;
        }

        public async Task<ArrivalsDB> Add(ArrivalsDB arrival, List<SerialsDB> serials)
        {
           
            EntityEntry<ArrivalsDB> result;
            try
            {
                result = await _context.Arrivals.AddAsync(arrival);
            }
            catch 
            {
                throw new Exception("Error al registrar la recepción");
            }
                
            int added;
            try
            {
                added = AddSerials(serials, arrival.Id).Result;
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
            int idSerial = _arrivalsService.GetLastIdArrivalSerials().Result;
            idSerial++;
            foreach (var s in serials)
            {
                var existingSerial = _serialService.GetSerialBySerial(s.Serial);
                var aux = existingSerial.Result != null ? existingSerial.Result.Id : 0;
                if (aux != 0)
                {
                    var newItem = new ArrivalSerials
                    {
                        Id = idSerial,
                        Serial = s.Serial.ToString(),
                        ArrivalID = id
                    };
                    idSerial++;

                    var res = await _arrivalsService.UpdateArrivalSerialsTable(newItem);
                    adds += res;
                }
            }
            return adds;
        }
    }
}
