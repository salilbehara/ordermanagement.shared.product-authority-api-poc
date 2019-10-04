using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_api.Application.Common;
using ordermanagement.shared.product_authority_api.Application.Queries.Products;
using Swashbuckle.AspNetCore.Annotations;

namespace ordermanagement.shared.product_authority_api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IQueryProcessor _queries;
        private readonly ICommandProcessor _commands;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IQueryProcessor queries, ICommandProcessor commands, ILogger<ProductsController> logger)
        {
            _queries = queries;
            _commands = commands;
            _logger = logger;
        }


        /// <summary>
        /// Get Product details for the supplied product id and effective start date.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(OperationId = "Product_GetProductBasedOnEffectiveStartDateAsync")]
        [ProducesResponseType(typeof(GetProductBasedOnEffectiveStartDateQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductBasedOnEffectiveStartDateAsync([FromQuery][Required]string productKey, DateTime? effectiveStartDate) => 
            Ok(await _queries.Process(new GetProductBasedOnEffectiveStartDateQuery(productKey, effectiveStartDate ?? DateTime.UtcNow)));


        /// <summary>
        /// Get all available Product statuses
        /// </summary>
        [HttpGet]
        [Route("status")]
        [SwaggerOperation(OperationId = "Product_GetAllProductStatusesAsync")]
        [ProducesResponseType(typeof(GetAllProductStatusesQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProductStatusesAsync() => Ok(await _queries.Process(new GetAllProductStatusesQuery()));


        /// <summary>
        /// Get all available Product types
        /// </summary>
        [HttpGet]
        [Route("types")]
        [SwaggerOperation(OperationId = "Product_GetAllProductTypesAsync")]
        [ProducesResponseType(typeof(GetAllProductTypesQueryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProductTypesAsync() => Ok(await _queries.Process(new GetAllProductTypesQuery()));


        /// <summary>
        /// Add a Product
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

                await _commands.Process(addProductCommand);

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
        [SwaggerOperation(OperationId = "Product_UpdateProductAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductAsync([FromBody]UpdateProductDto request)
        {
            try
            {
                var updateProductCommand = new UpdateProductCommand(request.ProductKey, request.EffectiveStartDate, request.ProductName, request.ProductDisplayName, request.PrintIssn, request.OnlineIssn,
                    request.ProductTypeCode, request.ProductStatusCode, request.PublisherProductCode, request.LegacyIdSpid);

                await _commands.Process(updateProductCommand);

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
