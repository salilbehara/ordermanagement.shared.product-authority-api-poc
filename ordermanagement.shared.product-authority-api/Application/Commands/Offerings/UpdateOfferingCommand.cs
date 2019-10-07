using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Offerings
{
    public class UpdateOfferingCommand : CommandEvent, IRequest
    {
        [Required, MaxLength(16)]
        public string ProductKey { get; }

        [Required, MaxLength(16)]
        public string OfferingKey { get; }

        public DateTime ChangeEffectiveDate { get; }

        [Required, MaxLength(4)]
        public string OfferingFormatCode { get; }

        [MaxLength(4)]
        public string OfferingPlatformCode { get; }

        [Required, MaxLength(4)]
        public string OfferingStatusCode { get; }

        [MaxLength(64)]
        public string OfferingEdition { get; }

        public UpdateOfferingCommand(string productKey, string offeringKey, DateTime changeEffectiveDate, string offeringFormatCode, string offeringPlatformCode, string offeringStatusCode, string offeringEdition)
        {
            //Add Precondition checks here if required. 
            //This along with property attributes will help avoid transmitting invalid commands to command handlers and enforce the fail fast principle.

            ProductKey = productKey;
            OfferingKey = offeringKey;
            ChangeEffectiveDate = changeEffectiveDate;
            OfferingFormatCode = offeringFormatCode;
            OfferingPlatformCode = offeringPlatformCode;
            OfferingStatusCode = offeringStatusCode;
            OfferingEdition = offeringEdition;
        }
    }
}
