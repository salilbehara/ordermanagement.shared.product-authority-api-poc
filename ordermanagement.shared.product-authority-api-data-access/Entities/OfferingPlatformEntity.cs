﻿using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api_data_access.Entities
{
    public partial class OfferingPlatformEntity
    {
        public OfferingPlatformEntity()
        {
            Offerings = new HashSet<OfferingEntity>();
        }

        public string OfferingPlatformCode { get; set; }
        public string OfferingPlatformName { get; set; }

        public virtual ICollection<OfferingEntity> Offerings { get; set; }
    }
}
