using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_api.Application.Queries.Products;
using Swashbuckle.AspNetCore.Annotations;

namespace ordermanagement.shared.product_authority_api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        /// <summary>
        /// Get product details for the supplied product key and effective start date.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(OperationId = "Product_GetProductBasedOnEffectiveStartDateAsync")]
        [ProducesResponseType(typeof(GetProductBasedOnEffectiveStartDateQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductBasedOnEffectiveStartDateAsync([FromQuery][Required]string productKey, DateTime? effectiveStartDate) => 
            Ok(await _mediator.Send(new GetProductBasedOnEffectiveStartDateQuery(productKey, effectiveStartDate ?? DateTime.UtcNow)));


        /// <summary>
        /// Get all available product statuses
        /// </summary>
        [HttpGet]
        [Route("Status")]
        [SwaggerOperation(OperationId = "Product_GetAllProductStatusesAsync")]
        [ProducesResponseType(typeof(GetAllProductStatusesQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProductStatusesAsync() => Ok(await _mediator.Send(new GetAllProductStatusesQuery()));


        /// <summary>
        /// Get all available product types
        /// </summary>
        [HttpGet]
        [Route("Types")]
        [SwaggerOperation(OperationId = "Product_GetAllProductTypesAsync")]
        [ProducesResponseType(typeof(GetAllProductTypesQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProductTypesAsync() => Ok(await _mediator.Send(new GetAllProductTypesQuery()));


        /// <summary>
        /// Add a new Product
        /// </summary>
        [HttpPost]
        [SwaggerOperation(OperationId = "Product_AddProductAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProductAsync([FromBody]AddProductDto request)
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
        /// Update an existing product
        /// </summary>
        [HttpPut]
        [SwaggerOperation(OperationId = "Product_UpdateProductAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductAsync([FromBody]UpdateProductDto request)
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
