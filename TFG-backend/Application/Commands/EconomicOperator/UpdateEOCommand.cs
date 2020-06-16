using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.EconomicOperator
{
    public class UpdateEOCommand : BaseCommand, IRequest<string>
    {
        public UpdateEOCommand(EORequestDTO dto, string requestSerialized, string requestObjectName) : base(requestObjectName)
        {
            Request = dto;
            RequestSerialized = requestSerialized;
        }

        public EORequestDTO Request { get; set; }
    }
}
