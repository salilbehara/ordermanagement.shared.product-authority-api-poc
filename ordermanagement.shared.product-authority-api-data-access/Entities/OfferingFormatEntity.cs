using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api_data_access.Entities
{
    public partial class OfferingFormatEntity
    {
        public OfferingFormatEntity()
        {
            Offerings = new HashSet<OfferingEntity>();
        }

        public string OfferingFormatCode { get; set; }
        public string OfferingFormatName { get; set; }

        public virtual ICollection<OfferingEntity> Offerings { get; set; }
    }
}
