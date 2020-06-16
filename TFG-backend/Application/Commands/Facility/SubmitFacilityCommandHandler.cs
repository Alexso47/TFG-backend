using Application.Commands.EconomicOperator;
using Application.Commands.Extensions;
using Application.Models;
using Domain;
using Domain.Filters;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Facility
{
    class SubmitFacilityCommandHandler : IRequestHandler<SubmitFacilityCommand, string>
    {
        private IFacilitiesRepository _facilitiesRepository;


        public SubmitFacilityCommandHandler(IFacilitiesRepository facilitiesRepository)
        {
            _facilitiesRepository = facilitiesRepository;
        }

        public async Task<string> Handle(SubmitFacilityCommand request, CancellationToken cancellationToken)
        {
            FacilityRequestDTO dto = request.Request;
            try
            {
                dto.ValidateObject("La request no puede ser null");
                LocalValidations(dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var facility = new Facilities();

            try
            {
                facility = new Facilities
                {
                    Id = dto.Id,
                    EOID = dto.EOID,
                    Name = dto.Name,
                    Description = dto.Description,
                    Address = dto.Address,
                    ZipCode = dto.ZipCode,
                    City = dto.City,
                    Country = dto.Country,
                    ActiveFrom = dto.ActiveFrom
                };
            }
            catch
            {
                throw new Exception("Error al formatear la Facility");
            }

            try
            {
                var added = _facilitiesRepository.Add(facility).Result;
                return added;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LocalValidations(FacilityRequestDTO dto)
        {
            dto.Id.ValidateString("El FID es necesario");
            dto.EOID.ValidateString("El EOID es necesario");
            dto.Name.ValidateString("El nombre del EO es necesario");
            dto.Address.ValidateString("La dirección es necesaria");
            dto.ZipCode.ValidateString("El código postal es necesario");
            dto.City.ValidateString("La ciudad es necesaria");
            dto.Country.ValidateString("El país es necesario");
            dto.ActiveFrom.ValidateObject("La fecha es necesaria");
        }
    }
}
