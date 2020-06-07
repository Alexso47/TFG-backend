using Domain;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SerialsRepository : ISerialsRepository
    {

        private readonly DBContext _context;

        private ISerialService _serialService;

        public SerialsRepository(DBContext context, ISerialService serialService)
        {
            _context = context;
            _serialService = serialService;
        }

        public async Task<SerialsDB> Add(string newSerial)
        {
            var existingSerial = _serialService.GetSerialBySerial(newSerial);
            if(existingSerial.Id != 0)
            {
                int id = _serialService.GetLastIdSerial().Result;

                var serial = new SerialsDB
                {
                    Id = id + 1,
                    Serial = newSerial
                };

                var result = await _context.Serials.AddAsync(serial);
                return result.Entity;
            }

            return new SerialsDB { };

        }

        //public Task<Facility> Get(int FacilityId)
        //{
        //    return _context.Facilities.FirstOrDefaultAsync(x => x.Id == FacilityId);
        //}

        //public Task<Facility> GetActiveUntilNull(string name)
        //{
        //    return _context.Facilities.FirstOrDefaultAsync(x => x.ActiveUntil == null && x.Name == name);
        //}

        //public Task<Facility> GetActive(string name)
        //{
        //    return _context.Facilities.FirstOrDefaultAsync(x => x.Name == name && (x.ActiveUntil == null || x.ActiveUntil >= DateTime.Today));
        //}

        //public async Task<Facility> Update(Facility Facility)
        //{
        //    var result = await Task.FromResult(_context.Facilities.Update(Facility));
        //    return result.Entity;
        //}

        //public bool Delete(Facility Facility)
        //{
        //    _context.Facilities.Remove(Facility);
        //    return true;
        //}

        //public Task<List<Facility>> GetAll(bool includeDeleted = false)
        //{
        //    if (includeDeleted)
        //    {
        //        return _context.Facilities.IgnoreQueryFilters().ToListAsync();
        //    }
        //    else
        //    {
        //        return _context.Facilities.ToListAsync();
        //    }
        //}

        //public Task<Facility> Get(int FacilityId, bool includeDeleted = false)
        //{
        //    if (includeDeleted)
        //    {
        //        return _context.Facilities.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == FacilityId);
        //    }
        //    else
        //    {
        //        return _context.Facilities.FirstOrDefaultAsync(x => x.Id == FacilityId);
        //    }
        //}
    }
}
