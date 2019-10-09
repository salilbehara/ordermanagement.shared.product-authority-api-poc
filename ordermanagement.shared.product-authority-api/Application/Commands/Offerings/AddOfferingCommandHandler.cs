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
    public class AddOfferingCommandHandler : AsyncRequestHandler<AddOfferingCommand>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public AddOfferingCommandHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        protected override async Task Handle(AddOfferingCommand request, CancellationToken cancellationToken)
        {
            var productId = request.ProductKey.DecodeKeyToId();
            var isProductActive = await _context.Products
                .AnyAsync(p => p.ProductId == productId &&
                               p.EffectiveStartDate <= request.OrderStartDate &&
                               p.EffectiveEndDate > request.OrderStartDate);

            if (!isProductActive)
            {
                throw new ValidationException($"No active product found for Product Key '{request.ProductKey}' and Order Effective date of '{request.OrderStartDate}'");
            }

            var offering = new OfferingEntity
            {
                EffectiveStartDate = request.OrderStartDate ?? DateTime.UtcNow,
                EffectiveEndDate = DateTime.MaxValue,
                ProductId = request.ProductKey.DecodeKeyToId(),
                OfferingFormatCode = request.OfferingFormatCode,
                OfferingPlatformCode = request.OfferingPlatformCode,
                OfferingStatusCode = request.OfferingStatusCode,
                OfferingEdition = request.OfferingEdition,
                AddedBy = "ProductAuthority",
                AddedOnUtc = DateTime.UtcNow
            };

            await _context.Offerings.AddAsync(offering);
            await _context.SaveChangesAndPublishEventsAsync(request.CommandEvents);
        }
    }
}