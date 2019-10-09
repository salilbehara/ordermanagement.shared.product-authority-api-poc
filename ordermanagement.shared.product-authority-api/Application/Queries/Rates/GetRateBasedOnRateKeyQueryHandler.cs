using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_api.Application.Models;
using ordermanagement.shared.product_authority_infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Rates
{
    public class GetRateBasedOnRateKeyQueryHandler : IRequestHandler<GetRateBasedOnRateKeyQuery, GetRateBasedOnRateKeyQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetRateBasedOnRateKeyQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetRateBasedOnRateKeyQueryDto> Handle(GetRateBasedOnRateKeyQuery request, CancellationToken cancellationToken)
        {
            var rateId = request.RateKey.DecodeKeyToId();

            var rates = await _context.Rates
                .AsNoTracking()
                .Where(r => r.RateId == rateId &&
                            r.EffectiveEndDate > request.OrderStartDate)
                .Include(r => r.RateType)
                .Include(r => r.DeliveryMethod)
                .Select(r => new RateDto
                {
                    RateKey = r.RateKey,
                    UnitRetailAmount = r.UnitRetailAmount,
                    CommissionAmount = r.CommissionAmount,
                    CommissionPercent = r.CommissionPercent,
                    CostAmount = r.CostAmount,
                    PostageAmount = r.PostageAmount,
                    TermLength = r.TermLength,
                    TermUnit = r.TermUnit,
                    Quantity = r.Quantity,
                    NewRenewalRateIndicator = r.NewRenewalRateIndicator,
                    EffortKey = r.EffortKey,
                    LegacyIdTitleNumber = r.LegacyIdTitleNumber,
                    ListCode = r.ListCode,
                    DeliveryMethod = r.DeliveryMethod.ToDeliveryMethodDto(),
                    RateType = r.RateType.ToRateTypeDto()
                })
                .ToListAsync();

            return new GetRateBasedOnRateKeyQueryDto { Rates = rates };
        }
    }
}
