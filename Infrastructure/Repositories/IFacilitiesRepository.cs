using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IFacilitiesRepository
    {
        Task<Facilities> Add(Facilities facility);

        //Task<Facility> Get(int facilityId);

        //Task<Facility> Get(int facilityId, bool includeDeleted);

        //Task<Facility> GetActiveUntilNull(string name);

        //Task<Facility> GetActive(string name);

        //Task<Facility> Update(Facility facility);

        //bool Delete(Facility facility);

        //Task<List<Facility>> GetAll(bool includeDeleted);
    }
}
