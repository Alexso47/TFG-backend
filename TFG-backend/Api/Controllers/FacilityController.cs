using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.dto.Dto;
using API.dto.Dto.Facility;
using API.dto.Dto.Invoice;
using API.Models;
using Application.Commands.Facility;
using Application.Commands.Invoice;
using Application.Queries.Dispatch;
using Application.Queries.Facility;
using Application.Queries.Invoice;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/facility")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IFacilityService _facilityService;

        public FacilityController(IMediator mediator, IFacilityService facilityService)
        {
            _mediator = mediator;
            _facilityService = facilityService;
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("FIDS")]
        [ProducesResponseType(typeof(List<string>), 200)]
        public async Task<IActionResult> FIDS()
        {
            var fids = _facilityService.GetFIDS();

            return Ok(fids.Result);
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("FIDSByEOID")]
        [ProducesResponseType(typeof(List<string>), 200)]
        public async Task<IActionResult> FIDSByEOID([FromQuery] FacilityByEOQueryString queryString)
        {
            var fids = _facilityService.GetFIDSByEO(queryString.EOID);

            return Ok(fids.Result);
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("GetFacilityInfo")]
        [ProducesResponseType(typeof(FacilityResult), 200)]
        public async Task<IActionResult> GetFacilityInfo([FromQuery] FacilityQueryString queryString)
        {
            var facility = _facilityService.GetFacilityById(queryString.Id);

            return Ok(facility.Result);
        }

        /// <param name="facilityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] FacilityRequest facilityRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = new FacilityResponse() { ResponseResult = new ResponseResult(facilityRequest.RequestHeader.RequestId) };

            try
            {
                FacilityRequestDTO dto = GetFacilityRequestDtoFromRequest(facilityRequest);

                result.Reference = new FacilityReferenceResponse
                {
                    FacilityNumber = dto.Id,
                };


                var command = new SubmitFacilityCommand(dto,
                    JsonConvert.SerializeObject(facilityRequest),
                    facilityRequest.GetType().Name);

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

        private FacilityRequestDTO GetFacilityRequestDtoFromRequest(FacilityRequest request)
        {
            var dto = new FacilityRequestDTO
            {
                Id = request.Id,
                EOID = request.EOID,
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
