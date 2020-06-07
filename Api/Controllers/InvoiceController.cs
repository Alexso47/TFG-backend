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
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("Summary")]
        [ProducesResponseType(typeof(PaginatedList<InvoiceResult>), 200)]
        public async Task<IActionResult> Summary([FromQuery] InvoiceQueryString queryString)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = new InvoiceQuery(
                queryString.CreationDateFrom,
                queryString.CreationDateTo,
                queryString.Id.Value,
                queryString.Price.Value,
                queryString.Currency,
                queryString.BuyerID,
                queryString.BuyerEU
                );

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <param name="invoiceDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] InvoiceRequest invoiceRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = new InvoiceResponse() { ResponseResult = new ResponseResult(invoiceRequest.RequestHeader.RequestId) };
            
            try
            {
                InvoiceRequestDTO dto = GetInvoiceRequestDtoFromRequest(invoiceRequest);

                result.Reference = new InvoiceReferenceResponse
                {
                    InvoiceNumber = dto.Id,
                    InvoiceDate = dto.InvoiceDate
                };


                var command = new SubmitInvoiceCommand(dto,
                    JsonConvert.SerializeObject(invoiceRequest),
                    invoiceRequest.GetType().Name);

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

        private InvoiceRequestDTO GetInvoiceRequestDtoFromRequest(InvoiceRequest request)
        {
            InvoiceRequestDTO dto = new InvoiceRequestDTO
            { 
                Serials = request.Serials,
                Id = request.Id,
                InvoiceDate = request.InvoiceDate,
                Price = request.Price,
                Currency = request.Currency,
                BuyerEU = request.BuyerEU,
                BuyerID = request.BuyerID,
                BuyerName = request.BuyerName,
                BuyerCountry = request.BuyerCountry,
                BuyerAddress = request.BuyerAddress,
                BuyerCity = request.BuyerCity,
                BuyerZipCode = request.BuyerZipCode
                //Id = request.Reference.Id,
                //InvoiceDate = request.Reference.InvoiceDate,
                //Price = request.Reference.Price,
                //Currency = request.Reference.Currency,
                //BuyerEU = request.Reference.BuyerEU,
                //BuyerID = request.Reference.BuyerID,
                //BuyerName = request.Reference.BuyerName,
                //BuyerCountry = request.Reference.BuyerCountry,
                //BuyerAddress = request.Reference.BuyerAddress,
                //BuyerCity = request.Reference.BuyerCity,
                //BuyerZipCode = request.Reference.BuyerZipCode
            };

            return dto;
        }
    }
}
