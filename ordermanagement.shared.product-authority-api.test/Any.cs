using System;
using System.Collections.Generic;
using System.Text;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure.Entities;

namespace ordermanagement.shared.product_authority_api.test
{
    public static class Any
    {
        private static readonly Random _random = new Random();

        public static bool Bool() => _random.Next(0, 2) == 1;

        public static DateTime DateTime() => new DateTime(Int(2019), Int(12), Int(28));

        public static decimal Decimal() => _random.Next(0, int.MaxValue);

        public static long Long() => _random.Next(0, int.MaxValue);

        public static string String(int length = 10)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[_random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

        public static int Int(int max = 1000) => _random.Next(max);
        public static short Short(int max = 1000) => (short)_random.Next(max);

        public static byte[] ByteArray(int length = 100)
        {
            var byteArray = new byte[length];

            _random.NextBytes(byteArray);

            return byteArray;
        }

        public static string[] StringArray(int length = 10)
        {
            var stringArray = new string[length];

            for (var i = 0; i < length; i++)
            {
                stringArray.SetValue(Any.String(), i);
            }

            return stringArray;
        }

        public static ProductEntity ProductEntity(string productKey, DateTime effectiveDate, DateTime? expirationDate = null)
        {
            var productStatus = Any.ProductStatusEntity();
            var productType = Any.ProductTypeEntity();
            return new ProductEntity
            {
                ProductId = productKey.DecodeKeyToId(),
                ProductKey = productKey,
                AddedBy = Any.String(),
                AddedOnUtc = System.DateTime.Now,
                EffectiveStartDate = effectiveDate,
                EffectiveEndDate = expirationDate ?? new DateTime(9999, 12, 31),
                LegacyIdSpid = Any.Int(),
                OnlineIssn = Any.String(8),
                PrintIssn = Any.String(8),
                ProductDisplayName = Any.String(),
                ProductName = Any.String(),
                PublisherId = Any.Long(),
                UpdatedBy = Any.String(),
                UpdatedOnUtc = System.DateTime.Now,
                ProductStatus = productStatus,
                ProductStatusCode = productStatus.ProductStatusCode,
                ProductType = productType,
                ProductTypeCode = productType.ProductTypeCode
            };
        }

        public static OfferingEntity OfferingEntity(string offeringKey, long productId, DateTime effectiveDate, DateTime? expirationDate = null)
        {
            var offeringFormat = Any.OfferingFormatEntity();
            var offeringPlatform = Any.OfferingPlatformEntity();
            var offeringStatus = Any.OfferingStatusEntity();

            return new OfferingEntity
            {
                ProductId = productId,
                AddedBy = Any.String(),
                AddedOnUtc = System.DateTime.Now,
                UpdatedBy = Any.String(),
                UpdatedOnUtc = System.DateTime.Now,
                EffectiveStartDate = effectiveDate,
                EffectiveEndDate = expirationDate ?? new DateTime(9999, 12, 31),
                OfferingEdition = Any.String(),
                OfferingFormat = offeringFormat,
                OfferingFormatCode = offeringFormat.OfferingFormatCode,
                OfferingId = offeringKey.DecodeKeyToId(),
                OfferingKey = offeringKey,
                OfferingPlatform = offeringPlatform,
                OfferingPlatformCode = offeringPlatform.OfferingPlatformCode,
                OfferingStatus = offeringStatus,
                OfferingStatusCode = offeringStatus.OfferingStatusCode

            };

        }

        public static ProductStatusEntity ProductStatusEntity() => new ProductStatusEntity
        {
            ProductStatusCode = Any.String(4),
            ProductStatusName = Any.String()
        };

        public static ProductTypeEntity ProductTypeEntity() => new ProductTypeEntity
        {
            ProductTypeCode = Any.String(4),
            ProductTypeName = Any.String()
        };

        public static OfferingFormatEntity OfferingFormatEntity() => new OfferingFormatEntity
        {
            OfferingFormatCode = Any.String(4),
            OfferingFormatName = Any.String()
        };

        public static OfferingPlatformEntity OfferingPlatformEntity() => new OfferingPlatformEntity
        {
            OfferingPlatformCode = Any.String(4),
            OfferingPlatformName = Any.String()
        };

        public static OfferingStatusEntity OfferingStatusEntity() => new OfferingStatusEntity
        {
            OfferingStatusCode = Any.String(4),
            OfferingStatusName = Any.String()
        };
    }
}
