using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.dto.Dto;
using API.dto.Dto.Dispatch;
using API.Models;
using Application.Commands.Dispatch;
using Application.Queries.Dispatch;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/dispatch")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DispatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("Summary")]
        [ProducesResponseType(typeof(PaginatedList<DispatchResult>), 200)]
        public async Task<IActionResult> Summary([FromQuery] DispatchQueryString queryString)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = new DispatchQuery(
                queryString.CreationDateFrom,
                queryString.CreationDateTo,
                queryString.Id.Value,
                queryString.FID,
                queryString.DestinationFID,
                queryString.TransportMode,
                queryString.Vehicle,
                queryString.DestinationEU
                );

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <param name="dispatchDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] DispatchRequest dispatchRequest)
       {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = new DispatchResponse() { ResponseResult = new ResponseResult(dispatchRequest.RequestHeader.RequestId) };

            try
            {
                DispatchRequestDTO dto = GetDispatchRequestDtoFromRequest(dispatchRequest);

                var command = new SubmitDispatchCommand(dto,
                    JsonConvert.SerializeObject(dispatchRequest),
                    dispatchRequest.GetType().Name);

                try
                {
                    var id = await _mediator.Send(command);

                    result.Reference = new DispatchReferenceResponse
                    {
                        DispatchNumber = id,
                        DispatchDate = dto.DispatchDate
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            catch (Exception e)
            {
                result.ResponseResult.Errors = new List<ErrorDetail>() {
                    new ErrorDetail(){
                         ErrorCode = "-1",
                          ErrorMessage = e.Message
                     }
                };
                return BadRequest(result);
            }

            return Ok(result);
        }

        private DispatchRequestDTO GetDispatchRequestDtoFromRequest(DispatchRequest request)
        {
            DispatchRequestDTO dto = new DispatchRequestDTO
            {
                Serials = request.Serials,
                FID = request.Facility,
                DispatchDate = request.DispatchDate,
                DestinationEU = request.DestinationEU,
                DestinationFID = request.DestinationFacility,
                DestinationName = request.DestinationName,
                DestinationCountry = request.DestinationCountry,
                DestinationAddress = request.DestinationAddress,
                DestinationCity = request.DestinationCity,
                DestinationZipCode = request.DestinationZipCode,
                TransportMode = request.TransportMode,
                Vehicle = request.Vehicle
            };

            return dto;
        }
    }
}
