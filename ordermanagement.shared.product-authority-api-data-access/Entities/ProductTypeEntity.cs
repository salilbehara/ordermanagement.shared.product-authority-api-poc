using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api_data_access.Entities
{
    public partial class ProductTypeEntity
    {
        public ProductTypeEntity()
        {
            Products = new HashSet<ProductEntity>();
        }

        public string ProductTypeCode { get; set; }
        public string ProductTypeName { get; set; }

        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
