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
    public class GetAllOfferingFormatsQueryHandler : IRequestHandler<GetAllOfferingFormatsQuery, GetAllOfferingFormatsQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetAllOfferingFormatsQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetAllOfferingFormatsQueryDto> Handle(GetAllOfferingFormatsQuery request, CancellationToken cancellationToken)
        {
            var offeringFormats = await _context.OfferingFormats
                .AsNoTracking()
                .Select(o => o.ToOfferingFormatDto())
                .ToListAsync();

            return new GetAllOfferingFormatsQueryDto { OfferingFormats = offeringFormats };
        }
    }
}
