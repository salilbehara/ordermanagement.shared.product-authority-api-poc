using MediatR;
using System;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Rates
{
    public class GetRateBasedOnOfferingKeyQuery : IRequest<GetRateBasedOnOfferingKeyQueryDto>
    {
        public string OfferingKey { get; private set; }
        public DateTime? OrderStartDate { get; private set; }

        public GetRateBasedOnOfferingKeyQuery(string offeringKey, DateTime orderStartDate)
        {
            OfferingKey = offeringKey;
            OrderStartDate = orderStartDate;
        }

        public GetRateBasedOnOfferingKeyQuery(string offeringKey) : this(offeringKey, DateTime.UtcNow)
        {
        }
    }
}
