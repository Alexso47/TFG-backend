using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Dispatch
{
    public class SubmitDispatchCommand : BaseCommand , IRequest<string>
    {
        public SubmitDispatchCommand(DispatchRequestDTO dto, string requestSerialized, string requestObjectName) : base(requestObjectName)
        {
            Request = dto;
            RequestSerialized = requestSerialized;
        }

        public DispatchRequestDTO Request { get; set; }
    }
}
