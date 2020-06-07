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

namespace Application.Queries.Arrival
{
    public class ArrivalQueryHandler : IRequestHandler<ArrivalQuery, PaginatedList<ArrivalResult>>
    {
        private readonly IArrivalService _arrivalService;

        public ArrivalQueryHandler(IArrivalService arrivalService)
        {
            _arrivalService = arrivalService;
        }

        public async Task<PaginatedList<ArrivalResult>> Handle(ArrivalQuery request, CancellationToken cancellationToken)
        {
            var filter = GetFilter(request);
            var arrivals = await _arrivalService.GetArrivals(filter);
            int total = await _arrivalService.GetTotalArrivals(filter);
            PaginatedList<ArrivalResult> result = CreateResultResponse(request, arrivals);
            result.Total = total;
            return result;
        }

        private ArrivalFilter GetFilter(ArrivalQuery request)
        {
            return new ArrivalFilter()
            {
                ArrivalDateFrom = request.DateFrom,
                ArrivalDateTo = request.DateTo,
                Id = request.Id.Value,
                FID = request.FID
            };
        }

        private PaginatedList<ArrivalResult> CreateResultResponse(ArrivalQuery request, List<Arrivals> result)
        {
            var arrivalsResult = new List<ArrivalResult>();
            var filteredList = result;

            foreach (var arrival in filteredList)
            {
                var arrivalResult = new ArrivalResult()
                {
                    Id = arrival.Id,
                    FID = arrival.FID,
                    Country = arrival.Country,
                    City = arrival.City,
                    Address = arrival.Address,
                    ZipCode = arrival.ZipCode,
                    ArrivalDate = arrival.ArrivalDate.Date,
                    Comments = arrival.Comments,
                    Serials = arrival.Serials,
                    SerialsJson = JsonConvert.SerializeObject(arrival.Serials)
                };

                arrivalsResult.Add(arrivalResult);
            }

            return new PaginatedList<ArrivalResult>(arrivalsResult);
        }
    }
}
