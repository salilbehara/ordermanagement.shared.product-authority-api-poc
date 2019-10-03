using ordermanagement.shared.product_authority_api.Application.Common;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public AddProductCommandHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task Execute(AddProductCommand command)
        {
            var request = new ProductEntity
            {
                LegacyIdSpid = command.LegacyIdSpid,
                OnlineIssn = command.OnlineIssn,
                PrintIssn = command.PrintIssn,
                ProductName = command.ProductName,
                ProductDisplayName = command.ProductDisplayName,
                ProductStatusCode = command.ProductStatusCode,
                ProductTypeCode = command.ProductTypeCode,
                PublisherId = command.PublisherId,
                PublisherProductCode = command.PublisherProductCode,
                AddedBy = "ProductAuthority"
            };

            await _context.Products.AddAsync(request);
            await _context.SaveChangesAsync();
        }
    }
}
