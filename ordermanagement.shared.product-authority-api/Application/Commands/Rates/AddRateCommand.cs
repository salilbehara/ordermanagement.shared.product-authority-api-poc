using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Rates
{
    public class AddRateCommand : CommandEvent, IRequest
    {
        [Required, MaxLength(16)]
        public string ProductKey { get; }

        [Required, MaxLength(16)]
        public string OfferingKey { get; }

        public DateTime OrderStartDate { get; }

        public DateTime OrderEndDate { get; }

        [Required]
        public long RateClassificationId { get; }

        [Range(0, 999999.99, ErrorMessage = "The field Unit Retail Amount must be between 0 and 999999.99.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Decimal value precision for Unit Retail Amount cannot exceed 2 decimal places")]
        public decimal? UnitRetailAmount { get; }

        [Range(0, 999999.99, ErrorMessage = "The field Commission Amount must be between 0 and 999999.99.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Decimal value precision for Commission Amount cannot exceed 2 decimal places")]
        public decimal? CommissionAmount { get; }

        [Range(0, 99.99, ErrorMessage = "The field Commission Percent must be between 0 and 99.99.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Decimal value precision for Commission Percent cannot exceed 2 decimal places")]
        public decimal? CommissionPercent { get; }

        [Range(0, 999999.99, ErrorMessage = "The field Cost Amount must be between 0 and 999999.99.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Decimal value precision for Cost Amount cannot exceed 2 decimal places")]
        public decimal? CostAmount { get; }

        [Range(0, 9999.99, ErrorMessage = "The field Postage Amount must be between 0 and 9999.99.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Decimal value precision for Postage Amount cannot exceed 2 decimal places")]
        public decimal? PostageAmount { get; }

        [Required, MaxLength(4)]
        public string DeliveryMethodCode { get; }

        [Required]
        public int TermLength { get; }

        [Required, MaxLength(4)]
        public string TermUnit { get; }

        [Required]
        public int Quantity { get; }

        [Required, MaxLength(1)]
        public string NewRenewalRateIndicator { get; }

        [MaxLength(64)]
        public string EffortKey { get; }

        public int? LegacyIdTitleNumber { get; }

        [MaxLength(4)]
        public string ListCode { get; }

        [MaxLength(4)]
        public string RateTypeCode { get; }

        public AddRateCommand(string productKey, string offeringKey, DateTime orderStartDate, DateTime orderEndDate, long rateClassificationId, decimal? unitRetailAmount, decimal? commissionAmount,
            decimal? commissionPercent, decimal? costAmount, decimal? postageAmount, string deliveryMethodCode, int termLength, string termUnit, int quantity, string newRenewalRateIndicator,
            string effortKey, int? legacyIdTitleNumber, string listCode, string rateTypeCode)
        {
            //Add Precondition checks here if required. 
            //This along with property attributes will help avoid transmitting invalid commands to command handlers and enforce the fail fast principle.

            ProductKey = productKey;
            OfferingKey = offeringKey;
            OrderStartDate = orderStartDate;
            OrderEndDate = orderEndDate;
            RateClassificationId = rateClassificationId;
            UnitRetailAmount = unitRetailAmount;
            CommissionAmount = commissionAmount;
            CommissionPercent = commissionPercent;
            CostAmount = costAmount;
            PostageAmount = postageAmount;
            DeliveryMethodCode = deliveryMethodCode;
            TermLength = termLength;
            TermUnit = termUnit;
            Quantity = quantity;
            NewRenewalRateIndicator = newRenewalRateIndicator;
            EffortKey = effortKey;
            LegacyIdTitleNumber = legacyIdTitleNumber;
            ListCode = listCode;
            RateTypeCode = rateTypeCode;
        }
    }
}
