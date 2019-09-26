using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ordermanagement.shared.product_authority_api.Models;
using ordermanagement.shared.product_authority_api.Models.Requests;
using ordermanagement.shared.product_authority_api.Models.Response;
using ordermanagement.shared.product_authority_api.ServiceAbstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace ordermanagement.shared.product_authority_api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get Product details for the supplied product id and date range.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(OperationId = "Product_GetProduct")]
        [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult GetProduct([FromQuery]GetProductRequest request)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                if ((request.ProductId ?? 0) == 0 && 
                     string.IsNullOrEmpty(request.ProductKey))
                {
                    return BadRequest();
                }

                var titleCatalogResponse = _productRepository.GetProduct(request);

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                titleCatalogResponse.ElapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, stopWatch.ElapsedMilliseconds);                

                return Ok(titleCatalogResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong in GetProduct");

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
        /// Add a Product
        /// </summary>
        [HttpPost]
        [SwaggerOperation(OperationId = "Product_AddProduct")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult AddProducts([FromBody]AddProductRequest request)
        {
            try
            {
                _productRepository.AddProduct(request);

                return Ok();
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
