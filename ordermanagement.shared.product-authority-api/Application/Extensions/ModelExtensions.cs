using ordermanagement.shared.product_authority_api.Application.Models;
using ordermanagement.shared.product_authority_infrastructure.Entities;

namespace ordermanagement.shared.product_authority_api.Application.Extensions
{
    public static class ModelExtensions
    {
        public static ProductTypeDto ToProductTypeDto(this ProductTypeEntity request) => new ProductTypeDto
        {
            ProductTypeCode = request.ProductTypeCode,
            ProductTypeName = request.ProductTypeName
        };

        public static ProductStatusDto ToProductStatusDto(this ProductStatusEntity request) => new ProductStatusDto
        {
            ProductStatusCode = request.ProductStatusCode,
            ProductStatusName = request.ProductStatusName
        };

        public static OfferingFormatDto ToOfferingFormatDto(this OfferingFormatEntity request) => new OfferingFormatDto
        {
            OfferingFormatCode = request.OfferingFormatCode,
            OfferingFormatName = request.OfferingFormatName
        };

        public static OfferingPlatformDto ToOfferingPlatformDto(this OfferingPlatformEntity request) => new OfferingPlatformDto
        {
            OfferingPlatformCode = request.OfferingPlatformCode,
            OfferingPlatformName = request.OfferingPlatformName
        };

        public static OfferingStatusDto ToOfferingStatusDto(this OfferingStatusEntity request) => new OfferingStatusDto
        {
            OfferingStatusCode = request.OfferingStatusCode,
            OfferingStatusName = request.OfferingStatusName
        };

        public static RateTypeDto ToRateTypeDto(this RateTypeEntity request) => new RateTypeDto
        {
            RateTypeCode = request.RateTypeCode,
            RateTypeName = request.RateTypeName
        };

        public static DeliveryMethodDto ToDeliveryMethodDto(this DeliveryMethodEntity request) => new DeliveryMethodDto
        {
            DeliveryMethodCode = request.DeliveryMethodCode,
            DeliveryMethodName = request.DeliveryMethodName
        };
    }
}
