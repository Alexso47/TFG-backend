using Application.Commands.Extensions;
using Domain;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Dispatch
{
    class SubmitDispatchCommandHandler : IRequestHandler<SubmitDispatchCommand, string>
    {
        private readonly IFacilityService _facilityService;
        private readonly IDispatchesRepository _dispatchesRepository;
        private readonly ISerialsRepository _serialsRepository;
        private readonly IDispatchService _dispatchService;


        public SubmitDispatchCommandHandler(IFacilityService facilityService, IDispatchesRepository dispatchesRepository, ISerialsRepository serialsRepository, IDispatchService dispatchService)
        {
            _facilityService = facilityService;
            _dispatchesRepository = dispatchesRepository;
            _dispatchService = dispatchService;
        }

        public async Task<string> Handle(SubmitDispatchCommand request, CancellationToken cancellationToken)
        {
            DispatchRequestDTO dto = request.Request;
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

            var origin = await _facilityService.GetFacilityById(dto.FID);
            if (origin == null)
            {
                throw new Exception("No existe el FID de origen");
            }

            Facilities destination = new Facilities();
            
            if (dto.DestinationEU == 1)
            {
                var res = _facilityService.GetFacilityById(dto.DestinationFID);
                if (res.Result != null)
                {
                    destination = res.Result;
                }
                else
                {
                    throw new Exception("No existe este FID europeo");
                }
            }
            else
            {
                var res = _facilityService.GetFacilityByFacility(dto.DestinationName , dto.DestinationCountry, dto.DestinationCity, dto.DestinationAddress, dto.DestinationZipCode);
                if (res.Result != null)
                {
                    destination = res.Result;
                }
                else
                {
                    throw new Exception("No existe este Facility");
                }
            }

            if (dto.FID == destination.Id)
            {
                throw new Exception("El destino no puede ser el mismo que el origen");
            }

            if (destination.Id != null)
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


                var newId = _dispatchService.GetLastIdDispatch().Result;

                var dispatch = new DispatchesDB
                {
                    Id = newId + 1,
                    FID = dto.FID,
                    DispatchDate = dto.DispatchDate.DateTime,
                    TransportMode = dto.TransportMode,
                    Vehicle = dto.Vehicle,
                    DestinationFID = destination.Id,
                    DestinationEU = Convert.ToBoolean(dto.DestinationEU)
                };
                try
                {
                    var added = _dispatchesRepository.Add(dispatch, serials).Result;
                    return added.Id.ToString();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Error al registrar el envio");
            }
        }

        private void LocalValidations(DispatchRequestDTO dto)
        {
            dto.FID.ValidateString("El Id de la Facility de envio es necesario");
            dto.DispatchDate.ValidateObject("La fecha del envio es necesaria");
            dto.DestinationEU.ValidateObject("Comprador en EU es necesario");
            if (dto.DestinationEU == 1)
            {
                dto.DestinationFID.ValidateString("Si el destinacion es europeo, el ID es necesario");
            }
            else
            {
                dto.DestinationName.ValidateString("El nombre de la destinacion es necesario para destinaciones extraeuropeos");
                dto.DestinationCountry.ValidateString("El pais de la destinacion es necesario para destinaciones extraeuropeos");
                dto.DestinationCity.ValidateString("La ciudad de la destinacion es necesario para destinaciones extraeuropeos");
                dto.DestinationAddress.ValidateString("La dirección de la destinacion es necesario para destinaciones extraeuropeos");
                dto.DestinationZipCode.ValidateString("El codigo postal de la destinacion es necesario para destinaciones extraeuropeos");
            }

            dto.TransportMode.ValidateString("El tipo de transporte es necesaria");
            dto.Vehicle.ValidateString("El vehiculo es necesaria");
        }
    }
}
