using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_api.Application.Models;
using ordermanagement.shared.product_authority_infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Offerings
{
    public class GetOfferingBasedOnOfferingKeyQueryHandler : IRequestHandler<GetOfferingBasedOnOfferingKeyQuery, GetOfferingBasedOnOfferingKeyQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetOfferingBasedOnOfferingKeyQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetOfferingBasedOnOfferingKeyQueryDto> Handle(GetOfferingBasedOnOfferingKeyQuery request, CancellationToken cancellationToken)
        {
            var offeringId = request.OfferingKey.DecodeKeyToId();

            var offerings = await _context.Offerings
                .AsNoTracking()
                .Where(o => o.OfferingId == offeringId &&
                            o.EffectiveEndDate > request.OrderStartDate)
                .Include(o => o.OfferingStatus)
                .Include(o => o.OfferingFormat)
                .Include(o => o.OfferingPlatform)
                .Select(o => new OfferingDto
                {
                    OfferingKey = o.OfferingKey,
                    OfferingEdition = o.OfferingEdition,
                    OfferingStatus = o.OfferingStatus.ToOfferingStatusDto(),
                    OfferingFormat = o.OfferingFormat.ToOfferingFormatDto(),
                    OfferingPlatform = o.OfferingPlatform.ToOfferingPlatformDto()
                })
                .ToListAsync();

            return new GetOfferingBasedOnOfferingKeyQueryDto { Offerings = offerings };
        }
    }
}
