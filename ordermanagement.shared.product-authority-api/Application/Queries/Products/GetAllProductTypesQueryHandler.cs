using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    public class GetAllProductTypesQueryHandler : IRequestHandler<GetAllProductTypesQuery, GetAllProductTypesQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetAllProductTypesQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetAllProductTypesQueryDto> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypes = await _context.ProductTypes
                .AsNoTracking()
                .Select(p => p.ToProductTypeDto())
                .ToListAsync();

            return new GetAllProductTypesQueryDto { ProductTypes = productTypes };
        }
    }
}
