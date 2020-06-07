using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Application.Queries.Dispatch;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
