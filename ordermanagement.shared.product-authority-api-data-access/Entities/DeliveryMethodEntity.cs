using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api_data_access.Entities
{
    public partial class DeliveryMethodEntity
    {
        public DeliveryMethodEntity()
        {
            Rates = new HashSet<RateEntity>();
        }

        public string DeliveryMethodCode { get; set; }
        public string DeliveryMethodName { get; set; }

        public virtual ICollection<RateEntity> Rates { get; set; }
    }
}
