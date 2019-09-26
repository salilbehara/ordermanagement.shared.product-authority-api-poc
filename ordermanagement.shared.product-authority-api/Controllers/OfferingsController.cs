using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ordermanagement.shared.product_authority_api.Models.Requests;
using ordermanagement.shared.product_authority_api.Models.Response;
using ordermanagement.shared.product_authority_api.ServiceAbstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace ordermanagement.shared.product_authority_api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class OfferingsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<OfferingsController> _logger;

        public OfferingsController(IProductRepository productRepository, ILogger<OfferingsController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get Product details for the supplied product id and date range.
        /// </summary>
        [HttpGet]
        [Route("Products")]
        [SwaggerOperation(OperationId = "Product_GetProducts")]
        [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts([FromQuery]GetProductRequest request)
        {
            try
            {
                var titleCatalogResponse = _productRepository.GetProduct(request);

                return Ok(titleCatalogResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong in GetProductDetails");

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
