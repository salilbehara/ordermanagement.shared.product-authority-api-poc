using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Common;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    public class GetProductBasedOnEffectiveStartDateQueryHandler : IQueryHandler<GetProductBasedOnEffectiveStartDateQuery, GetProductBasedOnEffectiveStartDateQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetProductBasedOnEffectiveStartDateQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetProductBasedOnEffectiveStartDateQueryDto> Execute(GetProductBasedOnEffectiveStartDateQuery query)
        {
            var productId = query.ProductKey.DecodeKeyToId();

            var product = await _context.Products
                .AsNoTracking()
                .Where(p => p.ProductId == productId &&
                            p.EffectiveStartDate <= query.EffectiveStartDate &&
                            p.EffectiveEndDate > query.EffectiveStartDate)
                .Include(p => p.ProductStatus)
                .Include(p => p.ProductType)
                .Select(p => new GetProductBasedOnEffectiveStartDateQueryDto
                {
                    ProductKey = p.ProductKey,
                    LegacyIdSpid = p.LegacyIdSpid,
                    OnlineIssn = p.OnlineIssn,
                    PrintIssn = p.PrintIssn,
                    ProductName = p.ProductName,
                    PublisherId = p.PublisherId,
                    PublisherProductCode = p.PublisherProductCode,
                    ProductStatus = new ProductStatusDto
                    {
                        ProductStatusCode = p.ProductStatus.ProductStatusCode,
                        ProductStatusName = p.ProductStatus.ProductStatusName
                    },
                    ProductType = new ProductTypeDto
                    {
                        ProductTypeCode = p.ProductType.ProductTypeCode,
                        ProductTypeName = p.ProductType.ProductTypeName
                    }
                })
                .FirstOrDefaultAsync();

            return product;
        }
    }
}
