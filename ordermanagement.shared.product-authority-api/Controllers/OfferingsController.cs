using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
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
        /// Get all active Product Offerings for the supplied product key and order start date.
        /// </summary>
        [HttpGet]
        [Route("Products/{productKey}/Offerings")]
        [SwaggerOperation(OperationId = "Offering_GetOfferingsBasedOnProductKeyAsync")]
        [ProducesResponseType(typeof(GetOfferingBasedOnProductKeyQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOfferingsBasedOnProductKeyAsync([FromRoute][Required]string productKey, DateTime? orderStartDate) => 
            Ok(await _mediator.Send(new GetOfferingBasedOnProductKeyQuery(productKey, orderStartDate ?? DateTime.UtcNow)));


        /// <summary>
        /// Get all active Product Offerings for the supplied offering key and order start date.
        /// </summary>
        [HttpGet]
        [Route("Offerings")]
        [SwaggerOperation(OperationId = "Offering_GetOfferingsBasedOnOfferingKeyAsync")]
        [ProducesResponseType(typeof(GetOfferingBasedOnOfferingKeyQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOfferingsBasedOnOfferingKeyAsync([FromQuery][Required]string offeringKey, DateTime? orderStartDate) =>
            Ok(await _mediator.Send(new GetOfferingBasedOnOfferingKeyQuery(offeringKey, orderStartDate ?? DateTime.UtcNow)));


        /// <summary>
        /// Get all available Offering Statuses
        /// </summary>
        [HttpGet]
        [Route("Offerings/Status")]
        [SwaggerOperation(OperationId = "Offering_GetAllOfferingStatusesAsync")]
        [ProducesResponseType(typeof(GetAllOfferingStatusesQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOfferingStatusesAsync() => Ok(await _mediator.Send(new GetAllOfferingStatusesQuery()));


        /// <summary>
        /// Get all available Offering Formats
        /// </summary>
        [HttpGet]
        [Route("Offerings/Formats")]
        [SwaggerOperation(OperationId = "Offering_GetAllOfferingFormatsAsync")]
        [ProducesResponseType(typeof(GetAllOfferingFormatsQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOfferingFormatsAsync() => Ok(await _mediator.Send(new GetAllOfferingFormatsQuery()));


        /// <summary>
        /// Get all available Offering Platforms
        /// </summary>
        [HttpGet]
        [Route("Offerings/Platforms")]
        [SwaggerOperation(OperationId = "Offering_GetAllOfferingPlatformsAsync")]
        [ProducesResponseType(typeof(GetAllOfferingPlatformsQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOfferingPlatformsAsync() => Ok(await _mediator.Send(new GetAllOfferingPlatformsQuery()));


        /// <summary>
        /// Add a Product
        /// </summary>
        [HttpPost]
        [Route("Products/{productKey}/Offerings")]
        [SwaggerOperation(OperationId = "Offering_AddOfferingAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOfferingAsync([FromQuery, Required]string productKey, [FromBody]AddProductDto request)
        {
            try
            {
                var addProductCommand = new AddProductCommand(request.ProductName, request.ProductDisplayName, request.PublisherId, request.PrintIssn,
                    request.OnlineIssn, request.ProductTypeCode, request.ProductStatusCode, request.PublisherProductCode, request.LegacyIdSpid);

                await _mediator.Send(addProductCommand);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong in AddProductAsync");

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
        /// Update a Product
        /// </summary>
        [HttpPut]
        [Route("Products/{productKey}/Offerings")]
        [SwaggerOperation(OperationId = "Offering_UpdateProductAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductAsync([FromQuery, Required]string productKey, [FromBody]UpdateProductDto request)
        {
            try
            {
                var updateProductCommand = new UpdateProductCommand(request.ProductKey, request.EffectiveStartDate, request.ProductName, request.ProductDisplayName, request.PrintIssn, request.OnlineIssn,
                    request.ProductTypeCode, request.ProductStatusCode, request.PublisherProductCode, request.LegacyIdSpid);

                await _mediator.Send(updateProductCommand);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong in AddProductAsync");

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
