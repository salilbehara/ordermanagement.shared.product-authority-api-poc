using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class UpdateProductCommandHandler : AsyncRequestHandler<UpdateProductCommand>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public UpdateProductCommandHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        protected override async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productId = request.ProductKey.DecodeKeyToId();
            DateTime productEffectiveEndDate;
            long productPublisherId;

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId &&
                                          p.EffectiveStartDate <= request.EffectiveStartDate &&
                                          p.EffectiveEndDate > request.EffectiveStartDate);

            if (product != null)
            {
                productEffectiveEndDate = product.EffectiveEndDate;
                productPublisherId = product.PublisherId;

                product.EffectiveEndDate = request.EffectiveStartDate;
                product.UpdatedBy = "ProductAuthority";
                product.UpdatedOnUtc = DateTime.UtcNow;

                _context.Update(product);
            }
            else
            {
                throw new ValidationException($"No product found for Product Key '{request.ProductKey}' and Product Effective date of '{request.EffectiveStartDate}'");
            }

            var newProduct = new ProductEntity
            {
                ProductId = productId,
                ProductKey = request.ProductKey,
                EffectiveStartDate = request.EffectiveStartDate,
                EffectiveEndDate = productEffectiveEndDate,
                LegacyIdSpid = request.LegacyIdSpid,
                OnlineIssn = request.OnlineIssn,
                PrintIssn = request.PrintIssn,
                ProductName = request.ProductName,
                ProductDisplayName = request.ProductDisplayName,
                ProductStatusCode = request.ProductStatusCode,
                ProductTypeCode = request.ProductTypeCode,
                PublisherId = productPublisherId,
                PublisherProductCode = request.PublisherProductCode,
                AddedBy = "ProductAuthority",
                AddedOnUtc = DateTime.UtcNow,
            };

            await _context.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }
    }
}
