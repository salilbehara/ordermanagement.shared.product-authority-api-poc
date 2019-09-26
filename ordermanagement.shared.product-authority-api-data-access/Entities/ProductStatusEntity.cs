using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api_data_access.Entities
{
    public partial class ProductStatusEntity
    {
        public ProductStatusEntity()
        {
            Products = new HashSet<ProductEntity>();
        }

        public string ProductStatusCode { get; set; }
        public string ProductStatusName { get; set; }

        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
