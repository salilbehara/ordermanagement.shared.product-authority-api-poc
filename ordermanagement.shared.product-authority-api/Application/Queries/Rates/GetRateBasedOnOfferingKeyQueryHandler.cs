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
    public class GetRateBasedOnOfferingKeyQueryHandler : IRequestHandler<GetRateBasedOnOfferingKeyQuery, GetRateBasedOnOfferingKeyQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetRateBasedOnOfferingKeyQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetRateBasedOnOfferingKeyQueryDto> Handle(GetRateBasedOnOfferingKeyQuery request, CancellationToken cancellationToken)
        {
            var offeringId = request.OfferingKey.DecodeKeyToId();

            var rates = await _context.Rates
                .AsNoTracking()
                .Where(r => r.OfferingId == offeringId &&
                            r.EffectiveStartDate <= request.OrderStartDate &&
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

            return new GetRateBasedOnOfferingKeyQueryDto { Rates = rates };
        }
    }
}
