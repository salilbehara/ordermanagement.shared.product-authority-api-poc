using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Common;
using ordermanagement.shared.product_authority_infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    public class GetAllProductStatusesQueryHandler : IQueryHandler<GetAllProductStatusesQuery, GetAllProductStatusesQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetAllProductStatusesQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetAllProductStatusesQueryDto> Execute(GetAllProductStatusesQuery query)
        {
            try
            {
                var productStatuses = await _context.ProductStatuses
                    .AsNoTracking()
                    .Select(p => new ProductStatus
                    {
                        ProductStatusCode = p.ProductStatusCode,
                        ProductStatusName = p.ProductStatusName
                    })
                    .ToListAsync();

                return new GetAllProductStatusesQueryDto { ProductStatuses = productStatuses };

            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
    }
}
