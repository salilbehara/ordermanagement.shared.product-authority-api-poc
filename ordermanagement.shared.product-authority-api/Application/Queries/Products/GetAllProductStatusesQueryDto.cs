using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    //Product ViewModels / DTOs(Data Transfer Objects) will be returned from the server-side to client apps.
    //This ViewModels hold the data the way the client app needs.

    public class GetAllProductStatusesQueryDto
    {
        public IEnumerable<ProductStatus> ProductStatuses { get; set; }
    }

    public class ProductStatus
    {
        public string ProductStatusCode { get; set; }
        public string ProductStatusName { get; set; }
    }
}