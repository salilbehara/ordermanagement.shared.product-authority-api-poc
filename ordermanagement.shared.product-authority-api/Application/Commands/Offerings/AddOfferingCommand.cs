using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Offerings
{
    public class AddOfferingCommand : CommandEvent, IRequest
    {
        [Required]
        public string ProductKey { get; }

        public DateTime? OrderStartDate { get; }

        [Required, MaxLength(4)]
        public string OfferingFormatCode { get; }

        [MaxLength(4)]
        public string OfferingPlatformCode { get; }

        [Required, MaxLength(4)]
        public string OfferingStatusCode { get; }

        [MaxLength(64)]
        public string OfferingEdition { get; }

        public AddOfferingCommand(string productKey, DateTime? orderStartDate, string offeringFormatCode, string offeringPlatformCode, string offeringStatusCode, string offeringEdition)
        {
            //Add Precondition checks here if required. 
            //This along with property attributes will help avoid transmitting invalid commands to command handlers and enforce the fail fast principle.

            ProductKey = productKey;
            OrderStartDate = orderStartDate;
            OfferingFormatCode = offeringFormatCode;
            OfferingPlatformCode = offeringPlatformCode;
            OfferingStatusCode = offeringStatusCode;
            OfferingEdition = offeringEdition;
        }
    }
}
