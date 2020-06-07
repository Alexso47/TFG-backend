using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IFacilityService
    {
        Task<Facilities> GetFacilityByFacility(string facilityName, string country, string city, string address, string zipcode);

        Task<Facilities> GetFacilityById(string facilityId);
    }
}
