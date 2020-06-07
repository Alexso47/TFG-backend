using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Application.Queries.Arrival;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
