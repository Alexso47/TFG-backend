using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Arrival
{
    public class SubmitArrivalCommand : BaseCommand , IRequest<string>
    {
        public SubmitArrivalCommand(ArrivalRequestDTO dto, string requestSerialized, string requestObjectName) : base(requestObjectName)
        {
            Request = dto;
            RequestSerialized = requestSerialized;
        }

        public ArrivalRequestDTO Request { get; set; }
    }
}
