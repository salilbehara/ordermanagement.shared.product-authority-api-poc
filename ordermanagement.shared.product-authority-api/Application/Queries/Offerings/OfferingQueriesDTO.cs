using System;
using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Offerings
{
    public class Offering
    {
        public string OfferingKey { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public string OfferingEdition { get; set; }

        public virtual OfferingFormat OfferingFormat { get; set; }
        public virtual OfferingStatus OfferingStatus { get; set; }
        public virtual OfferingPlatform OfferingPlatform { get; set; }
    }

    public class OfferingFormat
    {
        public string OfferingFormatCode { get; set; }
        public string OfferingFormatName { get; set; }
    }

    public class OfferingStatus
    {
        public string OfferingStatusCode { get; set; }
        public string OfferingStatusName { get; set; }
    }

    public class OfferingPlatform
    {
        public string OfferingPlatformCode { get; set; }
        public string OfferingPlatformName { get; set; }
    }
}
