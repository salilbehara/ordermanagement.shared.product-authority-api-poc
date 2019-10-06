using MediatR;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class AddProductCommandHandler : AsyncRequestHandler<AddProductCommand>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public AddProductCommandHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        protected override async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new ProductEntity
            {
                LegacyIdSpid = request.LegacyIdSpid,
                OnlineIssn = request.OnlineIssn,
                PrintIssn = request.PrintIssn,
                ProductName = request.ProductName,
                ProductDisplayName = request.ProductDisplayName,
                ProductStatusCode = request.ProductStatusCode,
                ProductTypeCode = request.ProductTypeCode,
                PublisherId = request.PublisherId,
                PublisherProductCode = request.PublisherProductCode,
                AddedBy = "ProductAuthority"
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
    }
}
