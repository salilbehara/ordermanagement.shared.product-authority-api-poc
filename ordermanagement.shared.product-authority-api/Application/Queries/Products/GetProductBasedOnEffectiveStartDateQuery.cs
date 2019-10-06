using MediatR;
using System;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    public class GetProductBasedOnEffectiveStartDateQuery : IRequest<GetProductBasedOnEffectiveStartDateQueryDto>
    {
        public string ProductKey { get; private set; }
        public DateTime? EffectiveStartDate { get; private set; }

        public GetProductBasedOnEffectiveStartDateQuery(string productKey, DateTime effectiveStartDate)
        {
            ProductKey = productKey;
            EffectiveStartDate = effectiveStartDate;
        }

        public GetProductBasedOnEffectiveStartDateQuery(string productKey) : this(productKey, DateTime.UtcNow)
        {
        }
    }
}
