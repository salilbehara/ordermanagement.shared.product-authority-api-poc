﻿using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api_data_access.Entities
{
    public partial class RateTypeEntity
    {
        public RateTypeEntity()
        {
            Rates = new HashSet<RateEntity>();
        }

        public string RateTypeCode { get; set; }
        public string RateTypeName { get; set; }

        public virtual ICollection<RateEntity> Rates { get; set; }
    }
}
