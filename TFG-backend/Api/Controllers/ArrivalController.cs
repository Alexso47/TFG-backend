using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.dto.Dto;
using API.dto.Dto.Arrival;
using API.Models;
using Application.Commands.Arrival;
using Application.Queries.Arrival;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/arrival")]
    [ApiController]
    public class ArrivalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArrivalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("Summary")]
        [ProducesResponseType(typeof(PaginatedList<ArrivalResult>), 200)]
        public async Task<IActionResult> Summary([FromQuery] ArrivalQueryString queryString)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = new ArrivalQuery(
                queryString.CreationDateFrom,
                queryString.CreationDateTo,
                queryString.Id.Value,
                queryString.FID
                );

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <param name="arrivalDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] ArrivalRequest arrivalRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = new ArrivalResponse() { ResponseResult = new ResponseResult(arrivalRequest.RequestHeader.RequestId) };

            try
            {
                ArrivalRequestDTO dto = GetArrivalRequestDtoFromRequest(arrivalRequest);

                var command = new SubmitArrivalCommand(dto,
                    JsonConvert.SerializeObject(arrivalRequest),
                    arrivalRequest.GetType().Name);

                try
                {
                    var id = await _mediator.Send(command);

                    result.Reference = new ArrivalReferenceResponse
                    {
                        ArrivalNumber = id,
                        ArrivalDate = dto.ArrivalDate
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

        private ArrivalRequestDTO GetArrivalRequestDtoFromRequest(ArrivalRequest request)
        {
            ArrivalRequestDTO dto = new ArrivalRequestDTO
            {
                Serials = request.Serials,
                FID = request.FID,
                ArrivalDate = request.ArrivalDate,
                Comments = request.Comments
            };

            return dto;
        }
    }
}
