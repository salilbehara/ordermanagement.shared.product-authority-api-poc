using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    public class GetAllProductStatusesQueryHandler : IRequestHandler<GetAllProductStatusesQuery, GetAllProductStatusesQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetAllProductStatusesQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetAllProductStatusesQueryDto> Handle(GetAllProductStatusesQuery request, CancellationToken cancellationToken)
        {
            var productStatuses = await _context.ProductStatuses
                .AsNoTracking()
                .Select(p => p.ToProductStatusDto())
                .ToListAsync();

            return new GetAllProductStatusesQueryDto { ProductStatuses = productStatuses };
        }
    }
}
