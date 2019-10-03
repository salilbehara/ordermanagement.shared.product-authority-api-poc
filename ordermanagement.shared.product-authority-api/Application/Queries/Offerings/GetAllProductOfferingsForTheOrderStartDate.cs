using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Offerings
{
    public class GetAllProductOfferingsForTheOrderStartDate
    {
        public string ProductKey { get; set; }
        public DateTime? OrderStartDate { get; set; }
    }
}
