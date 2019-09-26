using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Extensions;
using ordermanagement.shared.product_authority_api.Models;
using ordermanagement.shared.product_authority_api.Models.Requests;
using ordermanagement.shared.product_authority_api.Models.Response;
using ordermanagement.shared.product_authority_api_data_access;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ordermanagement.shared.product_authority_api.ServiceAbstractions
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public ProductRepository(ProductAuthorityDatabaseContext context)
        {
            _context = context;
        }

        public GetProductResponse GetProduct(GetProductRequest request)
        {
            IEnumerable<Offering> offerings = new List<Offering>();
            IEnumerable<Rate> rates = new List<Rate>();

            var productQuery = _context.Products
                .AsNoTracking()
                .Include("ProductStatus")
                .Include("ProductType");

            if (!string.IsNullOrEmpty(request.ProductKey))
            {
                productQuery = productQuery.Where(p => 
                    p.ProductKey == request.ProductKey &&
                    p.EffectiveStartDate <= request.EffectiveStartDate &&
                    p.EffectiveEndDate > request.EffectiveStartDate);
            }
            else
            {
                productQuery = productQuery.Where(p => 
                    p.ProductId == request.ProductId &&
                    p.EffectiveStartDate <= request.EffectiveStartDate &&
                    p.EffectiveEndDate > request.EffectiveStartDate);
            }

            var productEntity = productQuery.FirstOrDefault();
            var product = productEntity.MapToProduct();

            if (request.IncludeOfferingsAndRates)
            {
                offerings = GetProductOfferings(productEntity.ProductId, request.EffectiveStartDate);
                rates = GetOfferingRates(productEntity.ProductId, request.EffectiveStartDate);
            }

            return new GetProductResponse { Product = product, Offerings = offerings, Rates = rates };
        }

        private IEnumerable<Offering> GetProductOfferings(long productId, DateTime cutOffDate)
        {
            var offerings = _context.Offerings
                .AsNoTracking()
                .Where(o => o.ProductId == productId && o.EffectiveEndDate >= cutOffDate)
                .Include("OfferingFormat")
                .Include("OfferingStatus")
                .ToList();

            return offerings.MapToOfferings();
        }

        private IEnumerable<Rate> GetOfferingRates(long productId, DateTime cutOffDate)
        {

            var rates = _context.Rates
                .AsNoTracking()
                .Where(o => o.ProductId == productId && o.EffectiveEndDate >= cutOffDate)
                .Include("DeliveryMethod")
                .ToList();

            return rates.MapToRates();
        }

        public void AddProduct(AddProductRequest request)
        {
            if (request.ProductId != 0)
            {
                var product = _context.Products
                    .FirstOrDefault(p => p.ProductId == request.ProductId && 
                                            p.EffectiveStartDate <= request.EffectiveStartDate &&
                                            p.EffectiveEndDate > request.EffectiveStartDate);

                product.EffectiveEndDate = request.EffectiveStartDate;

                _context.Update(product);
                _context.SaveChanges();
            }

            _context.Products.Add(request.MapToProductEntity());
            _context.SaveChanges();
        }
    }
}
