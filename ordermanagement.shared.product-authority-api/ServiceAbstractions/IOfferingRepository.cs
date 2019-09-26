using ordermanagement.shared.product_authority_api.Models.Requests;
using ordermanagement.shared.product_authority_api.Models.Response;

namespace ordermanagement.shared.product_authority_api.ServiceAbstractions
{
    public interface IOfferingRepository
    {
        GetProductResponse GetOfferings(GetProductRequest request);
    }
}
