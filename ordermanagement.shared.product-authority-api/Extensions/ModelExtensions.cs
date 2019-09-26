using ordermanagement.shared.product_authority_api.Models;
using ordermanagement.shared.product_authority_api.Models.Requests;
using ordermanagement.shared.product_authority_api_data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ordermanagement.shared.product_authority_api.Extensions
{
    public static class ModelExtensions
    {
        public static Product MapToProduct(this ProductEntity product) =>

            new Product
            {
                ProductKey = product.ProductKey,
                EffectiveStartDate = product.EffectiveStartDate,
                EffectiveEndDate = product.EffectiveEndDate,
                LegacyIdSpid = product.LegacyIdSpid,
                OnlineIssn = product.OnlineIssn,
                PrintIssn = product.PrintIssn,
                ProductName = product.ProductName,
                ProductStatusCode = product.ProductStatus.ProductStatusName,
                ProductTypeCode = product.ProductType.ProductTypeName,
                PublisherId = product.PublisherId,
                PublisherProductCode = product.PublisherProductCode
            };

        public static ProductEntity MapToProductEntity(this AddProductRequest request) =>

            new ProductEntity
            {
                ProductId = request.ProductId,
                EffectiveStartDate = request.EffectiveStartDate,
                EffectiveEndDate = request.EffectiveEndDate,
                LegacyIdSpid = request.LegacyIdSpid,
                OnlineIssn = request.OnlineIssn,
                PrintIssn = request.PrintIssn,
                ProductName = request.ProductName,
                ProductStatusCode = request.ProductStatusCode,
                ProductTypeCode = request.ProductTypeCode,
                PublisherId = request.PublisherId,
                PublisherProductCode = request.PublisherProductCode,
                AddedBy = "ProductAuthority",
                AddedOnUtc = DateTime.UtcNow
            };

        public static IEnumerable<Offering> MapToOfferings(this IEnumerable<OfferingEntity> offerings) =>
             offerings.Select(o => o.MapOffering()).ToArray();

        public static Offering MapOffering(this OfferingEntity offering) =>
            new Offering
            {
                OfferingKey = offering.OfferingKey,
                OfferingFormat = offering.OfferingFormat.OfferingFormatName,
                OfferingStatus = offering.OfferingStatus.OfferingStatusName,
                EffectiveStartDate = offering.EffectiveStartDate,
                EffectiveEndDate = offering.EffectiveEndDate,
            };

        public static IEnumerable<Rate> MapToRates(this IEnumerable<RateEntity> rates) =>
             rates.Select(o => o.MapRate()).ToArray();

        public static Rate MapRate(this RateEntity rate) =>
            new Rate
            {
                RateKey = rate.RateKey,
                CommissionAmount = rate.CommissionAmount,
                CommissionPercent = rate.CommissionPercent,
                CostAmount = rate.CostAmount,
                DeliveryMethodCode = rate.DeliveryMethod.DeliveryMethodName,
                EffectiveStartDate = rate.EffectiveStartDate,
                EffectiveEndDate = rate.EffectiveEndDate,
                EffortKey = rate.EffortKey,
                LegacyIdTitleNumber = rate.LegacyIdTitleNumber,
                ListCode = rate.ListCode,
                NewRenewalRateIndicator = rate.NewRenewalRateIndicator,
                PostageAmount = rate.PostageAmount,
                Quantity = rate.Quantity,
                TermLength = rate.TermLength,
                TermUnit = rate.TermUnit,
                UnitRetailAmount = rate.UnitRetailAmount
            };
    }
}
