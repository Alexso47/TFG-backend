using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Arrival
{
    public class ArrivalQuery : IRequest<PaginatedList<ArrivalResult>>
    {
        public ArrivalQuery(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int id, string fid)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
            Id = id;
            FID = fid;
        }

        public DateTimeOffset? DateFrom { get; set; }

        public DateTimeOffset? DateTo { get; set; }

        public int? Id { get; set; }

        public string FID { get; set; }
    }
}
