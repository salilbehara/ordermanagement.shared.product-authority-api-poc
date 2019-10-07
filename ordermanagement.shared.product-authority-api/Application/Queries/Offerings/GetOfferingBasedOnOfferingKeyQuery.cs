using MediatR;
using System;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Offerings
{
    public class GetOfferingBasedOnOfferingKeyQuery : IRequest<GetOfferingBasedOnOfferingKeyQueryDto>
    {
        public string OfferingKey { get; private set; }
        public DateTime? OrderStartDate { get; private set; }

        public GetOfferingBasedOnOfferingKeyQuery(string offeringKey, DateTime orderStartDate)
        {
            OfferingKey = offeringKey;
            OrderStartDate = orderStartDate;
        }

        public GetOfferingBasedOnOfferingKeyQuery(string offeringKey) : this(offeringKey, DateTime.UtcNow)
        {
        }
    }
}
