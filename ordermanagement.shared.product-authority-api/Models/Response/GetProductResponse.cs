using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api.Models.Response
{
    public class GetProductResponse
    {
        public Product Product { get; set; }

        public IEnumerable<Offering> Offerings { get; set; }

        public IEnumerable<Rate> Rates { get; set; }

        public string ElapsedTime { get; set; }
    }
}
