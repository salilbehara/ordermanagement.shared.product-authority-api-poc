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
    public class GetRateBasedOnProductKeyQueryHandler : IRequestHandler<GetRateBasedOnProductKeyQuery, GetRateBasedOnProductKeyQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetRateBasedOnProductKeyQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetRateBasedOnProductKeyQueryDto> Handle(GetRateBasedOnProductKeyQuery request, CancellationToken cancellationToken)
        {
            var productId = request.ProductKey.DecodeKeyToId();

            var rates = await _context.Rates
                .AsNoTracking()
                .Where(r => r.ProductId == productId &&
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
                    DeliveryMethod = new DeliveryMethodDto
                    {
                        DeliveryMethodCode = r.DeliveryMethod.DeliveryMethodCode,
                        DeliveryMethodName = r.DeliveryMethod.DeliveryMethodName
                    },
                    RateType = new RateTypeDto
                    {
                        RateTypeCode = r.RateType.RateTypeCode,
                        RateTypeName = r.RateType.RateTypeName
                    }
                })
                .ToListAsync();

            return new GetRateBasedOnProductKeyQueryDto { Rates = rates };
        }
    }
}
