using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Offerings
{
    public class UpdateOfferingCommandHandler : AsyncRequestHandler<UpdateOfferingCommand>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public UpdateOfferingCommandHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        protected override async Task Handle(UpdateOfferingCommand request, CancellationToken cancellationToken)
        {
            var offeringId = request.OfferingKey.DecodeKeyToId();
            long productId;
            DateTime offeringEffectiveEndDate;

            var offering = await _context.Offerings
                .FirstOrDefaultAsync(o => o.OfferingId == offeringId &&
                                          o.EffectiveStartDate <= request.ChangeEffectiveDate &&
                                          o.EffectiveEndDate > request.ChangeEffectiveDate);

            if (offering != null)
            {
                offeringEffectiveEndDate = offering.EffectiveEndDate;
                productId = offering.ProductId;

                offering.EffectiveEndDate = request.ChangeEffectiveDate;
                offering.UpdatedBy = "ProductAuthority";
                offering.UpdatedOnUtc = DateTime.UtcNow;

                _context.Update(offering);
            }
            else
            {
                throw new ValidationException($"No offering found for Offering Key '{request.OfferingKey}' and Change Effective date of '{request.ChangeEffectiveDate}'");
            }

            var newOffering = new OfferingEntity
            {
                OfferingId = offeringId,
                EffectiveStartDate = request.ChangeEffectiveDate,
                EffectiveEndDate = offeringEffectiveEndDate,
                OfferingKey = request.OfferingKey,
                ProductId = productId,
                OfferingFormatCode = request.OfferingFormatCode,
                OfferingPlatformCode = request.OfferingPlatformCode,
                OfferingStatusCode = request.OfferingStatusCode,
                OfferingEdition = request.OfferingEdition,
                AddedBy = "ProductAuthority",
                AddedOnUtc = DateTime.UtcNow,
            };

            await _context.AddAsync(newOffering);
            await _context.SaveChangesAsync();
        }
    }
}
