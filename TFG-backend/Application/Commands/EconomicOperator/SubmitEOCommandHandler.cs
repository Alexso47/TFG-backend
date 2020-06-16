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

namespace Application.Queries.EconomicOperator
{
    class SubmitEOCommandHandler : IRequestHandler<SubmitEOCommand, string>
    {
        private IEORepository _eoRepository;


        public SubmitEOCommandHandler(IEORepository eoRepository)
        {
            _eoRepository = eoRepository;
        }

        public async Task<string> Handle(SubmitEOCommand request, CancellationToken cancellationToken)
        {
            EORequestDTO dto = request.Request;
            try
            {
                dto.ValidateObject("La request no puede ser null");
                LocalValidations(dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var eo = new EconomicOperators();

            try
            {
                eo = new EconomicOperators
                {
                    Id = dto.Id,
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
                throw new Exception("Error al formatear el EO");
            }

            try
            {
                var added = _eoRepository.Add(eo).Result;
                return added;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LocalValidations(EORequestDTO dto)
        {
            dto.Id.ValidateString("El EOID es necesario");
            dto.Name.ValidateObject("El nombre del EO es necesario");
            dto.Address.ValidateObject("La dirección es necesaria");
            dto.ZipCode.ValidateString("El código postal es necesario");
            dto.City.ValidateString("La ciudad es necesaria");
            dto.Country.ValidateString("El país es necesario");
            dto.ActiveFrom.ValidateObject("La fecha es necesaria");
        }
    }
}
