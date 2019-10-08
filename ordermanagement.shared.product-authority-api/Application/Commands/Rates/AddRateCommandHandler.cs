using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Commands.Rates;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Offerings
{
    public class AddRateCommandHandler : AsyncRequestHandler<AddRateCommand>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public AddRateCommandHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        protected override async Task Handle(AddRateCommand request, CancellationToken cancellationToken)
        {
            var offeringId = request.OfferingKey.DecodeKeyToId();
            var offering = await _context.Offerings
                .FirstOrDefaultAsync(o => o.OfferingId == offeringId &&
                                          o.EffectiveStartDate <= request.OrderStartDate &&
                                          o.EffectiveEndDate > request.OrderStartDate);

            if (offering == null)
            {
                throw new ValidationException($"No active offering found for Offering Key '{request.OfferingKey}' and Order Start date of '{request.OrderStartDate}'");
            }

            offering = await _context.Offerings
                .FirstOrDefaultAsync(o => o.OfferingId == offeringId &&
                                          o.EffectiveStartDate <= request.OrderEndDate);

            if (offering == null)
            {
                throw new ValidationException($"No active offering found for Offering Key '{request.OfferingKey}' and Order End date of '{request.OrderStartDate}'");
            }

            var rate = new RateEntity
            {
                EffectiveStartDate = request.OrderStartDate,
                EffectiveEndDate = request.OrderEndDate,
                OfferingId = offeringId,
                ProductId = request.ProductKey.DecodeKeyToId(),
                RateClassificationId = request.RateClassificationId,
                UnitRetailAmount = request.UnitRetailAmount,
                CommissionAmount = request.CommissionAmount,
                CommissionPercent = request.CommissionPercent,
                CostAmount = request.CostAmount,
                PostageAmount = request.PostageAmount,
                DeliveryMethodCode = request.DeliveryMethodCode,
                TermLength = request.TermLength,
                TermUnit = request.TermUnit,
                Quantity = request.Quantity,
                NewRenewalRateIndicator = request.NewRenewalRateIndicator,
                EffortKey = request.EffortKey,
                LegacyIdTitleNumber = request.LegacyIdTitleNumber,
                ListCode = request.ListCode,
                RateTypeCode = request.RateTypeCode,
                AddedBy = "ProductAuthority",
                AddedOnUtc = DateTime.UtcNow
            };

            await _context.Rates.AddAsync(rate);
            await _context.SaveChangesAndPublishEventsAsync(request.CommandEvents);
        }
    }
}