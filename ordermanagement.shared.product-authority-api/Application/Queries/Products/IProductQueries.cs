using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    public interface IProductQueries
    {
        Task<Product> GetProductAsync(string productKey, DateTime orderStartDate);

        Task<IEnumerable<ProductType>> GetProductTypesAsync();

        Task<IEnumerable<ProductStatus>> GetProductStatusesAsync();
    }
}
