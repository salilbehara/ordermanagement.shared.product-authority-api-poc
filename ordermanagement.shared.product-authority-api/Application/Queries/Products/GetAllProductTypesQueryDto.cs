using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    //Product ViewModels / DTOs(Data Transfer Objects) will be returned from the server-side to client apps.
    //This ViewModels hold the data the way the client app needs.

    public class GetAllProductTypesQueryDto
    {
        public IEnumerable<ProductType> ProductTypes { get; set; }
    }

    public class ProductType
    {
        public string ProductTypeCode { get; set; }
        public string ProductTypeName { get; set; }
    }
}