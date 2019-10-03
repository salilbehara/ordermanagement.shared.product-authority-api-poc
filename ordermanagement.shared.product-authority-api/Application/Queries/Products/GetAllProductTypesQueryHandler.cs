using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Common;
using ordermanagement.shared.product_authority_infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    public class GetAllProductTypesQueryHandler : IQueryHandler<GetAllProductTypesQuery, GetAllProductTypesQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetAllProductTypesQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetAllProductTypesQueryDto> Execute(GetAllProductTypesQuery query)
        {
            var productTypes = await _context.ProductTypes
                .AsNoTracking()
                .Select(p => new ProductType
                {
                    ProductTypeCode = p.ProductTypeCode,
                    ProductTypeName = p.ProductTypeName
                })
                .ToListAsync();

            return new GetAllProductTypesQueryDto { ProductTypes = productTypes };
        }
    }
}
