using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ordermanagement.shared.product_authority_api.Application.Commands.Offerings;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_api.test;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ordermanagement.shared.Offering_authority_api.test.Commands.Offerings
{
    public class UpdateOfferingCommandHandler_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private readonly IRequestHandler<UpdateOfferingCommand> _handler;
        private UpdateOfferingCommand _command;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private List<ProductEntity> _products;
        private List<OfferingEntity> _offerings;

        public UpdateOfferingCommandHandler_should()
        {

            _products = new List<ProductEntity>(new[]
            {
                Any.ProductEntity(Any.String(),new DateTime(2019, 02, 01))
            });
            _offerings = new List<OfferingEntity>(new[]
            {
                Any.OfferingEntity("key", _products.First().ProductId, _products.First().EffectiveStartDate)
            }); ;
            _context = GetContextWithData();

            _handler = new UpdateOfferingCommandHandler(_context);
            _command = new UpdateOfferingCommand(_products.First().ProductKey, _offerings.First().OfferingKey, _products.First().EffectiveStartDate.AddDays(1), Any.String(), Any.String(), Any.String(), Any.String());

        }

        [Fact]
        public async Task Set_Expiration_Date_On_Current_Effecive_Offering()
        {
            await _handler.Handle(_command, default);

            //verify existing record was updated
            var existingOffering = _offerings.First();
            Assert.Single(_context.Offerings, p =>
            {
                return
                    p.EffectiveStartDate == existingOffering.EffectiveStartDate &&
                    p.EffectiveEndDate == _command.ChangeEffectiveDate;
            });
        }

        [Fact]
        public async Task Add_New_Offering_Entity()
        {
            await _handler.Handle(_command, default);

            //verify new record was added
            Assert.Single(_context.Offerings, o =>
            {
                return
                    o.EffectiveStartDate == _command.ChangeEffectiveDate &&
                    o.EffectiveEndDate == new DateTime(9999, 12, 31) &&
                    o.ProductId == _command.ProductKey.DecodeKeyToId() &&
                    o.OfferingFormatCode == _command.OfferingFormatCode &&
                    o.OfferingPlatformCode == _command.OfferingPlatformCode &&
                    o.OfferingStatusCode == _command.OfferingStatusCode &&
                    o.OfferingEdition == _command.OfferingEdition &&
                    o.AddedBy == "ProductAuthority";
            });
        }

        [Fact]
        public async Task Throw_Validation_Error_If_Offering_Doesnt_ExistAsync()
        {
            _command = new UpdateOfferingCommand(_products.First().ProductKey, _offerings.First().OfferingKey + "nope", _products.First().EffectiveStartDate.AddDays(1), Any.String(), Any.String(), Any.String(), Any.String());
            _ = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(_command, default));
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;

            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);

            context.ProductStatuses.AddRange(new[]
            {
                new ProductStatusEntity {ProductStatusCode = "A", ProductStatusName = "StatusA"}
            });
            

            context.ProductTypes.AddRange(_products.Select(x => x.ProductType));
            context.ProductStatuses.AddRange(_products.Select(x => x.ProductStatus));
            context.Products.AddRange(_products);
            context.Offerings.AddRange(_offerings);

            context.SaveChanges();
            return context;
        }
    }
}
