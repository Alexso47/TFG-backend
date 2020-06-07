using Application.Models;
using Domain;
using Domain.Filters;
using Infrastructure.Services;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Serial
{
    public class SerialQueryHandler : IRequestHandler<SerialQuery, PaginatedList<SerialResult>>
    {
        private readonly ISerialService _serialService;

        public SerialQueryHandler(ISerialService serialService)
        {
            _serialService = serialService;
        }

        public async Task<PaginatedList<SerialResult>> Handle(SerialQuery request, CancellationToken cancellationToken)
        {
            var filter = GetFilter(request);
            var seriales = await _serialService.GetSerials(filter);
            int total = await _serialService.GetTotalSerials(filter);
            PaginatedList<SerialResult> result = CreateResultResponse(request, seriales);
            result.Total = total;
            return result;
        }

        private SerialFilter GetFilter(SerialQuery request)
        {
            return new SerialFilter()
            {
                Id = request.Id.Value,
                Serial = request.Serial
            };
        }
        private PaginatedList<SerialResult> CreateResultResponse(SerialQuery request, List<Serials> result)
        {
            var serialsResult = new List<SerialResult>();
            var filteredList = result;

            foreach (var serial in filteredList)
            {
                var serialResult = new SerialResult()
                {
                    Id = serial.Id,
                    Serial = serial.Serial,
                    ArrivalSerials = serial.ArrivalSerials,
                    DispatchSerials = serial.DispatchSerials,
                    InvoiceSerials = serial.InvoiceSerials,
                    RequestSerials = serial.RequestSerials
                };

                serialsResult.Add(serialResult);
            }

            return new PaginatedList<SerialResult>(serialsResult);
        }
    }
}
