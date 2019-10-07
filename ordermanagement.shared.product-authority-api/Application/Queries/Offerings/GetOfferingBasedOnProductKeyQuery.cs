using MediatR;
using System;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Offerings
{
    public class GetOfferingBasedOnProductKeyQuery : IRequest<GetOfferingBasedOnProductKeyQueryDto>
    {
        public string ProductKey { get; private set; }
        public DateTime? OrderStartDate { get; private set; }

        public GetOfferingBasedOnProductKeyQuery(string productKey, DateTime orderStartDate)
        {
            ProductKey = productKey;
            OrderStartDate = orderStartDate;
        }

        public GetOfferingBasedOnProductKeyQuery(string productKey) : this(productKey, DateTime.UtcNow)
        {
        }
    }
}
