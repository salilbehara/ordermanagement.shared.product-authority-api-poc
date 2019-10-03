using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Offerings
{
    public class OfferingQueriesHandler
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public OfferingQueriesHandler(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Offering>> Handle(GetAllProductOfferingsForTheOrderStartDate request)
        //{
        //    var productId = request.ProductKey.DecodeKeyToId();

        //    var offerings = await _context.Offerings
        //        .AsNoTracking()
        //        .Include("OfferingFormat")
        //        .Include("OfferingStatus")
        //        .Include("OfferingPlatform")
        //        .Where(p => p.ProductId == productId && p.EffectiveEndDate >= request.OrderStartDate)
        //        .ToListAsync();

        //    return offerings.MapToOfferings();
        //}
    }
}
