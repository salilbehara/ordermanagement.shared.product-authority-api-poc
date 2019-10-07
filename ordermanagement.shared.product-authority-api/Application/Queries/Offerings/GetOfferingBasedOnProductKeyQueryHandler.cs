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
    public class GetOfferingBasedOnProductKeyQueryHandler : IRequestHandler<GetOfferingBasedOnProductKeyQuery, GetOfferingBasedOnProductKeyQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetOfferingBasedOnProductKeyQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetOfferingBasedOnProductKeyQueryDto> Handle(GetOfferingBasedOnProductKeyQuery request, CancellationToken cancellationToken)
        {
            var productId = request.ProductKey.DecodeKeyToId();

            var offerings = await _context.Offerings
                .AsNoTracking()
                .Where(o => o.ProductId == productId &&
                            o.EffectiveEndDate > request.OrderStartDate)
                .Include(o => o.OfferingStatus)
                .Include(o => o.OfferingFormat)
                .Include(o => o.OfferingPlatform)
                .Select(o => new OfferingDto
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
                .ToListAsync();

            return new GetOfferingBasedOnProductKeyQueryDto { Offerings = offerings };
        }
    }
}
