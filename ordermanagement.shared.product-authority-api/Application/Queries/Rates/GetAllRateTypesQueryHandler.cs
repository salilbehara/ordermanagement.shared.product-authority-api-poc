using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Models;
using ordermanagement.shared.product_authority_infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Rates
{
    public class GetAllRateTypesQueryHandler : IRequestHandler<GetAllRateTypesQuery, GetAllRateTypesQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetAllRateTypesQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetAllRateTypesQueryDto> Handle(GetAllRateTypesQuery request, CancellationToken cancellationToken)
        {
            var rateTypes = await _context.RateTypes
                .AsNoTracking()
                .Select(r => new RateTypeDto
                {
                    RateTypeCode = r.RateTypeCode,
                    RateTypeName = r.RateTypeName
                })
                .ToListAsync();

            return new GetAllRateTypesQueryDto { RateTypes = rateTypes };
        }
    }
}
