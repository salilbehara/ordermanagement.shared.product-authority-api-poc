using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_api.Application.Queries.Models;
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

            var offering = await _context.Offerings
                .AsNoTracking()
                .Where(o => o.OfferingId == offeringId &&
                            o.EffectiveStartDate <= request.OrderStartDate &&
                            o.EffectiveEndDate > request.OrderStartDate)
                .Include(o => o.OfferingStatus)
                .Include(o => o.OfferingFormat)
                .Include(o => o.OfferingPlatform)
                .Select(o => new GetOfferingBasedOnOfferingKeyQueryDto
                {
                    OfferingKey = o.OfferingKey,
                    OfferingEdition = o.OfferingEdition,
                    OfferingStatus = new OfferingStatusDto
                    {
                        OfferingStatusCode = o.OfferingStatus.OfferingStatusCode,
                        OfferingStatusName = o.OfferingStatus.OfferingStatusName
                    },
                    OfferingFormat = new OfferingFormatDto
                    {
                        OfferingFormatCode = o.OfferingFormat.OfferingFormatCode,
                        OfferingFormatName = o.OfferingFormat.OfferingFormatName
                    },
                    OfferingPlatform = new OfferingPlatformDto
                    {
                        OfferingPlatformCode = o.OfferingPlatform.OfferingPlatformCode,
                        OfferingPlatformName = o.OfferingPlatform.OfferingPlatformName
                    }
                })
                .FirstOrDefaultAsync();

            return offering;
        }
    }
}
