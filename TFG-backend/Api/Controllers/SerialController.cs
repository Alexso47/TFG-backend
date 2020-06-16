using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Application.Queries.Serial;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/serial")]
    [ApiController]
    public class SerialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SerialController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("Summary")]
        [ProducesResponseType(typeof(PaginatedList<SerialResult>), 200)]
        public async Task<IActionResult> Summary([FromQuery] SerialQueryString queryString)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = new SerialQuery(
                queryString.Id,
                queryString.Serial
                );

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
