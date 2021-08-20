using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResultHelpers;
using System;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using ZipCodeValidationAndSearchApi.Application.SearchAddressByZipCode;

namespace ZipCodeValidationAndSearchApi.SearchAddressByZipCode.v1
{
    [Route("v1")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class SearchAddressByZipCodeController : Controller
    {
        private readonly IMediator _mediator;

        public SearchAddressByZipCodeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException("Mediator argument cannot be null.");
        }

        /// <summary>
        /// Retrieves the address based on an informed zip code
        /// </summary>
        /// <response code="200">Retrieves an OK status code along with data</response>
        /// <response code="400">Retrieves a Bad Request status code along with a failed operation result object</response>
        [ProducesResponseType(typeof(OperationResult<SearchAddressByZipCodeResponse>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(OperationResult<SearchAddressByZipCodeResponse>))]
        [HttpGet("addresses/{zipCode}/search")]
        public async Task<IActionResult> SearchAddressByZipCode(string zipCode, CancellationToken cancellationToken)
        {
            var request = new SearchAddressByZipCodeRequest { ZipCode = zipCode };

            var result = await _mediator.Send(request, cancellationToken);

            var response = result.GetResult();

            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
    }
}