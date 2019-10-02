using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    public class ProductQueries : IProductQueries
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public ProductQueries(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductAsync(string productKey, DateTime orderStartDate)
        {
            var productId = productKey.DecodeKeyToId();

            var product = await _context.Products
                .AsNoTracking()
                .Include("ProductStatus")
                .Include("ProductType")
                .FirstOrDefaultAsync(p => p.ProductId == productId &&
                                     p.EffectiveStartDate <= orderStartDate &&
                                     p.EffectiveEndDate > orderStartDate);

            return product.MapToProduct();
        }

        public async Task<IEnumerable<ProductStatus>> GetProductStatusesAsync()
        {
            var productStatus = await _context.ProductStatuses
                .AsNoTracking()
                .ToListAsync();

            return productStatus.MapToProductStatuses();
        }

        public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            var productTypes = await _context.ProductTypes
                .AsNoTracking()
                .ToListAsync();

            return productTypes.MapToProductTypes();
        }
    }
}
