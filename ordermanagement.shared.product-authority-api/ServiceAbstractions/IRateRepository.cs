using ordermanagement.shared.product_authority_api.Models.Requests;
using ordermanagement.shared.product_authority_api.Models.Response;

namespace ordermanagement.shared.product_authority_api.ServiceAbstractions
{
    public interface IRateRepository
    {
        GetProductResponse GetRates(GetProductRequest request);
    }
}
