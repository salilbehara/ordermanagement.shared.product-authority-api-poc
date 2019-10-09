using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_api.Application.Queries.Offerings;
using ordermanagement.shared.product_authority_infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    public class GetAllOfferingStatusesQueryHandler : IRequestHandler<GetAllOfferingStatusesQuery, GetAllOfferingStatusesQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetAllOfferingStatusesQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetAllOfferingStatusesQueryDto> Handle(GetAllOfferingStatusesQuery request, CancellationToken cancellationToken)
        {
            var offeringStatuses = await _context.OfferingStatuses
                .AsNoTracking()
                .Select(o => o.ToOfferingStatusDto())
                .ToListAsync();

            return new GetAllOfferingStatusesQueryDto { OfferingStatuses = offeringStatuses };
        }
    }
}
