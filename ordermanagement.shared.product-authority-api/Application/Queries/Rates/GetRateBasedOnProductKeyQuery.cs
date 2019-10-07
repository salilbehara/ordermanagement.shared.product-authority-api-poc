using MediatR;
using System;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Rates
{
    public class GetRateBasedOnProductKeyQuery : IRequest<GetRateBasedOnProductKeyQueryDto>
    {
        public string ProductKey { get; private set; }
        public DateTime? OrderStartDate { get; private set; }

        public GetRateBasedOnProductKeyQuery(string productKey, DateTime orderStartDate)
        {
            ProductKey = productKey;
            OrderStartDate = orderStartDate;
        }

        public GetRateBasedOnProductKeyQuery(string productKey) : this(productKey, DateTime.UtcNow)
        {
        }
    }
}
