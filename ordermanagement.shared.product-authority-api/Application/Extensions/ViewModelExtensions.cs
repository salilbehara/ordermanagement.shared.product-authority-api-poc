using ordermanagement.shared.product_authority_api.Application.Queries.Products;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ordermanagement.shared.product_authority_api.Application.Extensions
{
    public static class ViewModelExtensions
    {
        public static ProductStatus MapToProductStatus(this ProductStatusEntity productStatusEntity) =>
            new ProductStatus
            {
                ProductStatusCode = productStatusEntity.ProductStatusCode,
                ProductStatusName = productStatusEntity.ProductStatusName
            };

        public static ProductType MapToProductType(this ProductTypeEntity productTypeEntity) =>
            new ProductType
            {
                ProductTypeCode = productTypeEntity.ProductTypeCode,
                ProductTypeName = productTypeEntity.ProductTypeName
            };

        public static Product MapToProduct(this ProductEntity product) =>
            new Product
            {
                ProductKey = product.ProductKey,
                LegacyIdSpid = product.LegacyIdSpid,
                OnlineIssn = product.OnlineIssn,
                PrintIssn = product.PrintIssn,
                ProductName = product.ProductName,
                PublisherId = product.PublisherId,
                PublisherProductCode = product.PublisherProductCode,
                ProductStatus = product.ProductStatus?.MapToProductStatus(),
                ProductType = product.ProductType?.MapToProductType()
            };

        public static IEnumerable<ProductStatus> MapToProductStatuses(this IEnumerable<ProductStatusEntity> productStatuses) => 
            productStatuses.Select(s => s.MapToProductStatus()).ToArray();

        public static IEnumerable<ProductType> MapToProductTypes(this IEnumerable<ProductTypeEntity> productTypes) => 
            productTypes.Select(s => s.MapToProductType()).ToArray();
    }
}
