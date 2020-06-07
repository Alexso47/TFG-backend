using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FacilitiesRepository : IFacilitiesRepository
    {

        private readonly DBContext _context;

        public FacilitiesRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Facilities> Add(Facilities Facility)
        {
            var result = await _context.Facilities.AddAsync(Facility);
            return result.Entity;
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
