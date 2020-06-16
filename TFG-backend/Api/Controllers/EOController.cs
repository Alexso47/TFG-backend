using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using API.dto.Dto;
using API.dto.Dto.Arrival;
using API.dto.Dto.EconomicOperator;
using API.Models;
using Application.Commands.Arrival;
using Application.Commands.EconomicOperator;
using Application.Queries.Arrival;
using Application.Queries.EconomicOperator;
using Application.Queries.EO;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/EO")]
    [ApiController]
    public class EOController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IEconomicOperatorService _economicOperatorService;


        public EOController(IMediator mediator, IEconomicOperatorService economicOperatorService)
        {
            _mediator = mediator;
            _economicOperatorService = economicOperatorService;
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("EOIDS")]
        [ProducesResponseType(typeof(List<string>), 200)]
        public async Task<IActionResult> EOIDS()
        {
            var eoids = _economicOperatorService.GetEOIDS();

            return Ok(eoids.Result);
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("GetEOInfo")]
        [ProducesResponseType(typeof(EOResult), 200)]
        public async Task<IActionResult> GetEOInfo([FromQuery] EOQueryString queryString)
        {
            var eoids = _economicOperatorService.GetEO(queryString.Id);

            return Ok(eoids.Result);
        }

        /// <param name="eoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] EORequest eoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = new EOResponse() { ResponseResult = new ResponseResult(eoRequest.RequestHeader.RequestId) };

            try
            {
                EORequestDTO dto = GetEORequestDtoFromRequest(eoRequest);

                result.Reference = new EOReferenceResponse
                {
                    EONumber = dto.Id,
                };


                var command = new SubmitEOCommand(dto,
                    JsonConvert.SerializeObject(eoRequest),
                    eoRequest.GetType().Name);

                try
                {
                    var confirmationCode = await _mediator.Send(command);
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

        /// <param name="eoDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] EORequest eoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = new EOResponse() { ResponseResult = new ResponseResult(eoRequest.RequestHeader.RequestId) };

            try
            {
                EORequestDTO dto = GetEORequestDtoFromRequest(eoRequest);

                result.Reference = new EOReferenceResponse
                {
                    EONumber = dto.Id,
                };

                var command = new UpdateEOCommand(dto,
                    JsonConvert.SerializeObject(eoRequest),
                    eoRequest.GetType().Name);

                try
                {
                    var confirmationCode = await _mediator.Send(command);
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

        private EORequestDTO GetEORequestDtoFromRequest(EORequest request)
        {
            EORequestDTO dto = new EORequestDTO
            {
                Id = request.Id,
                Description = request.Description,
                ActiveFrom = request.ActiveFrom,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                Name = request.Name,
                ZipCode = request.ZipCode
            };

            return dto;
        }
    }
}
