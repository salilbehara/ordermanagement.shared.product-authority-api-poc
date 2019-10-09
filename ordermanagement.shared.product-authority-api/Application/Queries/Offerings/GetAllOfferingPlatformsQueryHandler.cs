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
    public class GetAllOfferingPlatformsQueryHandler : IRequestHandler<GetAllOfferingPlatformsQuery, GetAllOfferingPlatformsQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetAllOfferingPlatformsQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetAllOfferingPlatformsQueryDto> Handle(GetAllOfferingPlatformsQuery request, CancellationToken cancellationToken)
        {
            var offeringPlatforms = await _context.OfferingPlatforms
                .AsNoTracking()
                .Select(o => o.ToOfferingPlatformDto())
                .ToListAsync();

            return new GetAllOfferingPlatformsQueryDto { OfferingPlatforms = offeringPlatforms };
        }
    }
}
