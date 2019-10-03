using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Common;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public UpdateProductCommandHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task Execute(UpdateProductCommand command)
        {
            var productId = command.ProductKey.DecodeKeyToId();
            DateTime productEffectiveEndDate;
            long productPublisherId;

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId && 
                                          p.EffectiveStartDate <= command.EffectiveStartDate &&
                                          p.EffectiveEndDate > command.EffectiveStartDate);

            if (product != null)
            {
                productEffectiveEndDate = product.EffectiveEndDate;
                productPublisherId = product.PublisherId;

                product.EffectiveEndDate = command.EffectiveStartDate;
                product.UpdatedBy = "ProductAuthority";
                product.UpdatedOnUtc = DateTime.UtcNow;

                _context.Update(product);
            }
            else
            {
                //Throw validation error
                return;
            }

            var request = new ProductEntity
            {
                ProductId = productId,
                ProductKey = command.ProductKey,
                EffectiveStartDate = command.EffectiveStartDate,
                EffectiveEndDate = productEffectiveEndDate,
                LegacyIdSpid = command.LegacyIdSpid,
                OnlineIssn = command.OnlineIssn,
                PrintIssn = command.PrintIssn,
                ProductName = command.ProductName,
                ProductDisplayName = command.ProductDisplayName,
                ProductStatusCode = command.ProductStatusCode,
                ProductTypeCode = command.ProductTypeCode,
                PublisherId = productPublisherId,
                PublisherProductCode = command.PublisherProductCode,
                AddedBy = "ProductAuthority",
                AddedOnUtc = DateTime.UtcNow,
            };

            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
        }
    }
}
