using Application.Commands.Extensions;
using Domain;
using Domain.DB;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Arrival
{
    class SubmitArrivalCommandHandler : IRequestHandler<SubmitArrivalCommand, string>
    {
        private readonly IFacilityService _facilityService;
        private readonly IArrivalsRepository _arrivalsRepository;
        private readonly ISerialsRepository _serialsRepository;
        private readonly IArrivalService _arrivalService;


        public SubmitArrivalCommandHandler(IFacilityService facilityService, IArrivalsRepository arrivalsRepository, ISerialsRepository serialsRepository, IArrivalService arrivalService)
        {
            _facilityService = facilityService;
            _arrivalsRepository = arrivalsRepository;
            _arrivalService = arrivalService;
        }

        public async Task<string> Handle(SubmitArrivalCommand request, CancellationToken cancellationToken)
        {
            ArrivalRequestDTO dto = request.Request;
            try
            {
                dto.ValidateObject("La request no puede ser null");
                LocalValidations(dto);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if(dto.Serials.Count == 0)
            {
                throw new Exception("No hay seriales que registrar. Se ha cancelado la operacion.");
            }

            var arrivalF = await _facilityService.GetFacilityById(dto.FID);
            if (arrivalF == null)
            {
                throw new Exception("No existe el FID de recepción");
            }

            if (arrivalF.Id != null)
            {
                List<SerialsDB> serials = new List<SerialsDB>();

                foreach(var s in dto.Serials)
                {
                    if (s != "" && s != null && s != "[]")
                    {
                        serials.Add(new SerialsDB
                        {
                            Serial = s
                        });
                    }
                }


                var newId = _arrivalService.GetLastIdArrival().Result;

                var arrival = new ArrivalsDB
                {
                    Id = newId + 1,
                    FID = arrivalF.Id,
                    Comments = dto.Comments,
                    ArrivalDate = dto.ArrivalDate
                };
                try
                {
                    var added = _arrivalsRepository.Add(arrival, serials).Result;
                    return added.Id.ToString();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Error al registrar la recepción");
            }
        }

        private void LocalValidations(ArrivalRequestDTO dto)
        {
            dto.FID.ValidateString("El Id de la Facility de recepción es necesario");
            dto.ArrivalDate.ValidateObject("La fecha del recepción es necesaria");
            dto.Serials.ValidateObject("No hay seriales que registrar");
        }
    }
}
