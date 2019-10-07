using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Models;
using ordermanagement.shared.product_authority_infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Rates
{
    public class GetAllDeliveryMethodsQueryHandler : IRequestHandler<GetAllDeliveryMethodsQuery, GetAllDeliveryMethodsQueryDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public GetAllDeliveryMethodsQueryHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetAllDeliveryMethodsQueryDto> Handle(GetAllDeliveryMethodsQuery request, CancellationToken cancellationToken)
        {
            var deliveryMethods = await _context.DeliveryMethods
                .AsNoTracking()
                .Select(r => new DeliveryMethodDto
                {
                    DeliveryMethodCode = r.DeliveryMethodCode,
                    DeliveryMethodName = r.DeliveryMethodName
                })
                .ToListAsync();

            return new GetAllDeliveryMethodsQueryDto { DeliveryMethods = deliveryMethods };
        }
    }
}
