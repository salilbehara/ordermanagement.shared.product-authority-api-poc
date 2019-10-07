using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ordermanagement.shared.product_authority_api.Application.Commands.Offerings;
using ordermanagement.shared.product_authority_api.Application.Queries.Offerings;
using Swashbuckle.AspNetCore.Annotations;

namespace ordermanagement.shared.product_authority_api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/")]
    [Produces("application/json")]
    public class OfferingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OfferingsController> _logger;

        public OfferingsController(IMediator mediator, ILogger<OfferingsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        /// <summary>
        /// Get all active product offerings for the supplied product key and order start date.
        /// </summary>
        [HttpGet]
        [Route("Products/{productKey}/Offerings")]
        [SwaggerOperation(OperationId = "Offering_GetOfferingsBasedOnProductKeyAsync")]
        [ProducesResponseType(typeof(GetOfferingBasedOnProductKeyQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOfferingsBasedOnProductKeyAsync([FromRoute, Required] string productKey, DateTime? orderStartDate) => 
            Ok(await _mediator.Send(new GetOfferingBasedOnProductKeyQuery(productKey, orderStartDate ?? DateTime.UtcNow)));


        /// <summary>
        /// Get all active offerings for the supplied offering key and order start date.
        /// </summary>
        [HttpGet]
        [Route("Offerings/{offeringKey}")]
        [SwaggerOperation(OperationId = "Offering_GetOfferingsBasedOnOfferingKeyAsync")]
        [ProducesResponseType(typeof(GetOfferingBasedOnOfferingKeyQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOfferingsBasedOnOfferingKeyAsync([FromRoute, Required] string offeringKey, DateTime? orderStartDate) =>
            Ok(await _mediator.Send(new GetOfferingBasedOnOfferingKeyQuery(offeringKey, orderStartDate ?? DateTime.UtcNow)));


        /// <summary>
        /// Get all available offering statuses
        /// </summary>
        [HttpGet]
        [Route("Offerings/Status")]
        [SwaggerOperation(OperationId = "Offering_GetAllOfferingStatusesAsync")]
        [ProducesResponseType(typeof(GetAllOfferingStatusesQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOfferingStatusesAsync() => Ok(await _mediator.Send(new GetAllOfferingStatusesQuery()));


        /// <summary>
        /// Get all available offering formats
        /// </summary>
        [HttpGet]
        [Route("Offerings/Formats")]
        [SwaggerOperation(OperationId = "Offering_GetAllOfferingFormatsAsync")]
        [ProducesResponseType(typeof(GetAllOfferingFormatsQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOfferingFormatsAsync() => Ok(await _mediator.Send(new GetAllOfferingFormatsQuery()));


        /// <summary>
        /// Get all available offering platforms
        /// </summary>
        [HttpGet]
        [Route("Offerings/Platforms")]
        [SwaggerOperation(OperationId = "Offering_GetAllOfferingPlatformsAsync")]
        [ProducesResponseType(typeof(GetAllOfferingPlatformsQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOfferingPlatformsAsync() => Ok(await _mediator.Send(new GetAllOfferingPlatformsQuery()));


        /// <summary>
        /// Create a new offering for the product specified by the product key
        /// </summary>
        [HttpPost]
        [Route("Products/{productKey}/Offerings")]
        [SwaggerOperation(OperationId = "Offering_AddOfferingAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOfferingAsync([FromQuery, Required]string productKey, [FromBody]AddOfferingDto request)
        {
            try
            {
                var addOfferingCommand = new AddOfferingCommand(productKey, request.OrderStartDate, request.OfferingFormatCode, request.OfferingPlatformCode, request.OfferingStatusCode, request.OfferingEdition);

                await _mediator.Send(addOfferingCommand);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong in AddOfferingAsync");

                var errorResponse = new ProblemDetails()
                {
                    Title = "An unexpected error occurred. Please try again later.",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }


        /// <summary>
        /// Update an existing offering
        /// </summary>
        [HttpPut]
        [Route("Products/{productKey}/Offerings/{offeringKey}")]
        [SwaggerOperation(OperationId = "Offering_UpdateOfferingAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOfferingAsync([FromRoute, Required]string productKey, [FromRoute, Required]string offeringKey, [FromBody]UpdateOfferingDto request)
        {
            try
            {
                var updateOfferingCommand = new UpdateOfferingCommand(productKey, offeringKey, request.ChangeEffectiveDate ?? DateTime.UtcNow, 
                    request.OfferingFormatCode, request.OfferingPlatformCode, request.OfferingStatusCode, request.OfferingEdition);

                await _mediator.Send(updateOfferingCommand);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong in UpdateOfferingAsync");

                var errorResponse = new ProblemDetails()
                {
                    Title = "An unexpected error occurred. Please try again later.",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
