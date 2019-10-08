using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ordermanagement.shared.product_authority_api.Application.Commands.Offerings;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_api.Application.Commands.Rates;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ordermanagement.shared.product_authority_api.test.Commands.Products
{
    public class UpdateRateCommandHandler_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private readonly IRequestHandler<UpdateRateCommand> _handler;
        private UpdateRateCommand _command;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private List<ProductEntity> _products;
        private List<OfferingEntity> _offerings;
        private List<RateEntity> _rates;

        public UpdateRateCommandHandler_should()
        {
            
            
            _products = new List<ProductEntity>(new[]
            {
                Any.ProductEntity(Any.String(),new DateTime(2019, 02, 01))
            });
            _offerings = new List<OfferingEntity>(new[]
            {
                Any.OfferingEntity(Any.String(), _products.First().ProductId, _products.First().EffectiveStartDate)
            });
            _rates = new List<RateEntity>(new[]
            {
                Any.RateEntity("key", _products.First().ProductId, _offerings.First().OfferingId, _offerings.First().EffectiveStartDate),
            });
            _context = GetContextWithData();

            _handler = new UpdateRateCommandHandler(_context);
            _command = new UpdateRateCommand(_products.First().ProductKey, _offerings.First().OfferingKey, _rates.First().RateKey, _offerings.First().EffectiveStartDate.AddDays(1), _offerings.First().EffectiveEndDate, Any.Long(), Any.Decimal(), Any.Decimal(), Any.Decimal(), Any.Decimal(), Any.Decimal(), Any.String(), Any.Int(), Any.String(), Any.Int(), Any.String(), Any.String(), Any.Int(), Any.String(), Any.String());
        }

        [Fact]
        public async Task Add_New_Rate_Entity()
        {
            await _handler.Handle(_command, default);
            Assert.Single(_context.Rates, r =>
            {
                return
                    r.CommissionAmount == _command.CommissionAmount &&
                    r.CommissionPercent == _command.CommissionPercent &&
                    r.CostAmount == _command.CostAmount &&
                    r.DeliveryMethodCode == _command.DeliveryMethodCode &&
                    r.EffectiveEndDate == _command.OrderEndDate &&
                    r.EffectiveStartDate == _command.OrderStartDate &&
                    r.EffortKey == _command.EffortKey &&
                    r.LegacyIdTitleNumber == _command.LegacyIdTitleNumber &&
                    r.ListCode == _command.ListCode &&
                    r.NewRenewalRateIndicator == _command.NewRenewalRateIndicator &&
                    r.OfferingId == _command.OfferingKey.DecodeKeyToId() &&
                    r.PostageAmount == _command.PostageAmount &&
                    r.ProductId == _command.ProductKey.DecodeKeyToId() &&
                    r.Quantity == _command.Quantity &&
                    r.RateClassificationId == _command.RateClassificationId &&
                    r.RateTypeCode == _command.RateTypeCode &&
                    r.TermLength == _command.TermLength &&
                    r.TermUnit == _command.TermUnit &&
                    r.UnitRetailAmount == _command.UnitRetailAmount;
            });
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;

            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);

            context.Products.AddRange(_products);
            context.Offerings.AddRange(_offerings);
            context.Rates.AddRange(_rates);

            context.SaveChanges();
            return context;
        }
    }
}
