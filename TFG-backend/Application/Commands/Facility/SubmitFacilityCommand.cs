
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Facility
{
    public class SubmitFacilityCommand : BaseCommand, IRequest<string>
    {
        public SubmitFacilityCommand(FacilityRequestDTO dto, string requestSerialized, string requestObjectName) : base(requestObjectName)
        {
            Request = dto;
            RequestSerialized = requestSerialized;
        }

        public FacilityRequestDTO Request { get; set; }
    }
}
