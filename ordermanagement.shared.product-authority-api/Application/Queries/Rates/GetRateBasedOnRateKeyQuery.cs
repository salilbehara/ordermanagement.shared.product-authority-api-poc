using MediatR;
using System;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Rates
{
    public class GetRateBasedOnRateKeyQuery : IRequest<GetRateBasedOnRateKeyQueryDto>
    {
        public string RateKey { get; private set; }
        public DateTime? OrderStartDate { get; private set; }

        public GetRateBasedOnRateKeyQuery(string rateKey, DateTime orderStartDate)
        {
            RateKey = rateKey;
            OrderStartDate = orderStartDate;
        }

        public GetRateBasedOnRateKeyQuery(string rateKey) : this(rateKey, DateTime.UtcNow)
        {
        }
    }
}
