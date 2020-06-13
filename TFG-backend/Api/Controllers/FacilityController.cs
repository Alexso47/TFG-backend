using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.dto.Dto;
using API.dto.Dto.Invoice;
using API.Models;
using Application.Commands.Invoice;
using Application.Queries.Dispatch;
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

    }
}
