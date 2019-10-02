using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_infrastructure.Entities
{
    public partial class OfferingStatusEntity
    {
        public OfferingStatusEntity()
        {
            Offerings = new HashSet<OfferingEntity>();
        }

        public string OfferingStatusCode { get; set; }
        public string OfferingStatusName { get; set; }

        public virtual ICollection<OfferingEntity> Offerings { get; set; }
    }
}
