using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_api.Application.Queries.Rates;
using Swashbuckle.AspNetCore.Annotations;

namespace ordermanagement.shared.product_authority_api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/")]
    [Produces("application/json")]
    public class RatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RatesController> _logger;

        public RatesController(IMediator mediator, ILogger<RatesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        /// <summary>
        /// Get all active rate details for the supplied rate key and order start date.
        /// </summary>
        [HttpGet]
        [Route("Rates/{rateKey}")]
        [SwaggerOperation(OperationId = "Rate_GetRateBasedOnRateKeyAsync")]
        [ProducesResponseType(typeof(GetRateBasedOnRateKeyQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRateBasedOnRateKeyAsync([FromRoute, Required] string rateKey, DateTime? orderStartDate) =>
            Ok(await _mediator.Send(new GetRateBasedOnRateKeyQuery(rateKey, orderStartDate ?? DateTime.UtcNow)));


        /// <summary>
        /// Get all active offering rates for the supplied offering key and order start date.
        /// </summary>
        [HttpGet]
        [Route("Offerings/{offeringKey}/Rates")]
        [SwaggerOperation(OperationId = "Rate_GetRateBasedOnOfferingKeyAsync")]
        [ProducesResponseType(typeof(GetRateBasedOnOfferingKeyQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRateBasedOnOfferingKeyAsync([FromRoute, Required]string offeringKey, DateTime? orderStartDate) =>
            Ok(await _mediator.Send(new GetRateBasedOnOfferingKeyQuery(offeringKey, orderStartDate ?? DateTime.UtcNow)));


        /// <summary>
        /// Get all active product rates for the supplied product key and order start date.
        /// </summary>
        [HttpGet]
        [Route("Products/{productKey}/Rates")]
        [SwaggerOperation(OperationId = "Rate_GetRateBasedOnProductKeyAsync")]
        [ProducesResponseType(typeof(GetRateBasedOnProductKeyQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRateBasedOnProductKeyAsync([FromRoute, Required]string productKey, DateTime? orderStartDate) =>
            Ok(await _mediator.Send(new GetRateBasedOnProductKeyQuery(productKey, orderStartDate ?? DateTime.UtcNow)));


        /// <summary>
        /// Get all available delivery methods
        /// </summary>
        [HttpGet]
        [Route("Rates/DeliveryMethods")]
        [SwaggerOperation(OperationId = "Rate_GetAllDeliveryMethodsAsync")]
        [ProducesResponseType(typeof(GetAllDeliveryMethodsQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDeliveryMethodsAsync() => Ok(await _mediator.Send(new GetAllDeliveryMethodsQuery()));


        /// <summary>
        /// Get all available rate types
        /// </summary>
        [HttpGet]
        [Route("Rates/RateTypes")]
        [SwaggerOperation(OperationId = "Rate_GetAllRateTypesAsync")]
        [ProducesResponseType(typeof(GetAllRateTypesQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRateTypesAsync() => Ok(await _mediator.Send(new GetAllRateTypesQuery()));


        ///// <summary>
        ///// Add anRate to the Product
        ///// </summary>
        //[HttpPost]
        //[Route("Products/{productKey}/Offerings")]
        //[SwaggerOperation(OperationId = "Offering_AddOfferingAsync")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> AddOfferingAsync([FromQuery, Required]string productKey, [FromBody]AddProductDto request)
        //{
        //    try
        //    {
        //        var addProductCommand = new AddProductCommand(request.ProductName, request.ProductDisplayName, request.PublisherId, request.PrintIssn,
        //            request.OnlineIssn, request.ProductTypeCode, request.ProductStatusCode, request.PublisherProductCode, request.LegacyIdSpid);

        //        await _mediator.Send(addProductCommand);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Something went wrong in AddProductAsync");

        //        var errorResponse = new ProblemDetails()
        //        {
        //            Title = "An unexpected error occurred. Please try again later.",
        //            Status = StatusCodes.Status500InternalServerError,
        //            Detail = ex.Message
        //        };

        //        return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        //    }
        //}


        ///// <summary>
        ///// Update an existingRate
        ///// </summary>
        //[HttpPut]
        //[Route("Products/{productKey}/Offerings")]
        //[SwaggerOperation(OperationId = "Offering_UpdateProductAsync")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> UpdateProductAsync([FromQuery, Required]string productKey, [FromBody]UpdateProductDto request)
        //{
        //    try
        //    {
        //        var updateProductCommand = new UpdateProductCommand(request.ProductKey, request.EffectiveStartDate, request.ProductName, request.ProductDisplayName, request.PrintIssn, request.OnlineIssn,
        //            request.ProductTypeCode, request.ProductStatusCode, request.PublisherProductCode, request.LegacyIdSpid);

        //        await _mediator.Send(updateProductCommand);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Something went wrong in AddProductAsync");

        //        var errorResponse = new ProblemDetails()
        //        {
        //            Title = "An unexpected error occurred. Please try again later.",
        //            Status = StatusCodes.Status500InternalServerError,
        //            Detail = ex.Message
        //        };

        //        return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        //    }
        //}
    }
}
