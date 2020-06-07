using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Serial
{
    public class SerialQuery : IRequest<PaginatedList<SerialResult>>
    {
        public SerialQuery(int? id, string serial)
        {
            Id = id;
            Serial = serial;
        }

        public int? Id { get; set; }

        public string Serial { get; set; }
    }
}
