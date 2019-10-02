using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ordermanagement.shared.product_authority_api.Application.Queries.Products;
using Swashbuckle.AspNetCore.Annotations;

namespace ordermanagement.shared.product_authority_api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductQueries _productQueries;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductQueries productQueries, ILogger<ProductsController> logger)
        {
            _productQueries = productQueries;
            _logger = logger;
        }

        /// <summary>
        /// Get Product details for the supplied product id and date range.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(OperationId = "Product_GetProduct")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductAsync([FromQuery][Required]string ProductKey, DateTime? OrderStartDate)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                if (string.IsNullOrEmpty(ProductKey))
                {
                    return BadRequest();
                }

                var response = await _productQueries.GetProductAsync(ProductKey, OrderStartDate ?? DateTime.UtcNow);

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                var elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, stopWatch.ElapsedMilliseconds);                

                return Ok(response);
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

        ///// <summary>
        ///// Add a Product
        ///// </summary>
        //[HttpPost]
        //[SwaggerOperation(OperationId = "Product_AddProduct")]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        //public IActionResult AddProducts([FromBody]AddProductRequest request)
        //{
        //    try
        //    {
        //        _productRepository.AddProduct(request);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Something went wrong in GetProductDetails");

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
