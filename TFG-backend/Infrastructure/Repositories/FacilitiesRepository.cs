using Domain;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FacilitiesRepository : IFacilitiesRepository
    {

        private readonly DBContext _context;
        private IFacilityService _facilityService;
        private IEconomicOperatorService _economicOperatorService;


        public FacilitiesRepository(DBContext context, IFacilityService facilityService, IEconomicOperatorService economicOperatorService)
        {
            _context = context;
            _facilityService = facilityService;
            _economicOperatorService = economicOperatorService;
        }

        public async Task<string> Add(Facilities facility)
        {
            var correctEOID = await _economicOperatorService.GetEOById(facility.EOID);

            if (correctEOID != null || correctEOID == "")
            {
                var exist = await _facilityService.GetFacilityById(facility.Id);
                if (exist == null)
                {
                    try
                    {
                        var result = await _context.Facilities.AddAsync(facility);
                        await _context.SaveChangesAsync();
                        return result.Entity.Id;
                    }
                    catch
                    {
                        throw new Exception("Error creando el Facility");
                    }

                }
                else
                {
                    throw new Exception("Ya existe este Facility");
                }
            }
            else
            {
                throw new Exception("El EOID asociado no existe");
            }
           
        }

    }
}
