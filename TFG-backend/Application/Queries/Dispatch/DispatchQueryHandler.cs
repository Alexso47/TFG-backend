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

namespace Application.Queries.Dispatch
{
    public class DispatchQueryHandler : IRequestHandler<DispatchQuery, PaginatedList<DispatchResult>>
    {
        private readonly IDispatchService _dispatchService;

        public DispatchQueryHandler(IDispatchService dispatchService)
        {
            _dispatchService = dispatchService;
        }

        public async Task<PaginatedList<DispatchResult>> Handle(DispatchQuery request, CancellationToken cancellationToken)
        {
            var filter = GetFilter(request);
            var dispatches = await _dispatchService.GetDispatches(filter);
            int total = await _dispatchService.GetTotalDispatches(filter);
            PaginatedList<DispatchResult> result = CreateResultResponse(request, dispatches);
            result.Total = total;
            return result;
        }

        private DispatchFilter GetFilter(DispatchQuery request)
        {
            return new DispatchFilter()
            {
                DispatchDateFrom = request.DateFrom,
                DispatchDateTo = request.DateTo,
                Id = request.Id.Value,
                FID = request.FID,
                DestinationFID = request.DestinationFID,
                TransportMode = request.TransportMode,
                Vehicle = request.Vehicle,
                DestinationEU = request.DestinationEU
            };
        }

        private PaginatedList<DispatchResult> CreateResultResponse(DispatchQuery request, List<Dispatches> result)
        {
            var dispatchesResult = new List<DispatchResult>();
            var filteredList = result;

            foreach (var dispatch in filteredList)
            {
                var dispatchResult = new DispatchResult()
                {
                    Id = dispatch.Id,
                    FID = dispatch.FID,
                    DispatchDate = dispatch.DispatchDate.Date,
                    DestinationEU = dispatch.DestinationEU,
                    DestinationFID = dispatch.DestinationFID,
                    TransportMode = dispatch.TransportMode,
                    Vehicle = dispatch.Vehicle,
                    Serials = dispatch.Serials,
                    SerialsJson = JsonConvert.SerializeObject(dispatch.Serials),
                    DestinationCountry = dispatch.DestinationCountry,
                    DestinationCity = dispatch.DestinationCity,
                    DestinationAddress = dispatch.DestinationAddress,
                    DestinationZipCode = dispatch.DestinationZipCode

                };

                dispatchesResult.Add(dispatchResult);
            }

            return new PaginatedList<DispatchResult>(dispatchesResult);
        }
    }
}
