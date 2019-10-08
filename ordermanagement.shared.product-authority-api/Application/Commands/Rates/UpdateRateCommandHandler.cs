using MediatR;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Rates
{
    public class UpdateRateCommandHandler : AsyncRequestHandler<UpdateRateCommand>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public UpdateRateCommandHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        protected override async Task Handle(UpdateRateCommand request, CancellationToken cancellationToken)
        {
            //Need clarification on how to validate the time range for Rates

            var rateId = request.RateKey.DecodeKeyToId();
            var offeringId = request.OfferingKey.DecodeKeyToId();
            var productId = request.ProductKey.DecodeKeyToId();

            var newRate = new RateEntity
            {
                RateId = rateId,
                EffectiveStartDate = request.OrderStartDate,
                EffectiveEndDate = request.OrderEndDate,
                RateKey = request.RateKey,
                OfferingId = offeringId,
                ProductId = productId,
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

            await _context.AddAsync(newRate);
            await _context.SaveChangesAsync();
        }
    }
}
