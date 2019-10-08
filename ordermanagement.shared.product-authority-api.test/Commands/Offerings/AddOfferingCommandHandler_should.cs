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
    public class AddOfferingCommandHandler_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private readonly IRequestHandler<AddOfferingCommand> _handler;
        private AddOfferingCommand _command;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private List<ProductEntity> _products;

        public AddOfferingCommandHandler_should()
        {

            _products = new List<ProductEntity>(new[]
                        {
                Any.ProductEntity(Any.String(),new DateTime(2019, 02, 01))
            });
            _context = GetContextWithData();

            _handler = new AddOfferingCommandHandler(_context);
            _command = new AddOfferingCommand(_products.First().ProductKey, _products.First().EffectiveStartDate, Any.String(), Any.String(), Any.String(), Any.String());

        }

        [Fact]
        public async Task Add_Offering_To_Context()
        {
            await _handler.Handle(_command, default);
            Assert.Single(_context.Offerings, o =>
            {
                return
                    o.EffectiveStartDate == _command.OrderStartDate &&
                    o.EffectiveEndDate == DateTime.MaxValue &&
                    o.ProductId == _command.ProductKey.DecodeKeyToId() &&
                    o.OfferingFormatCode == _command.OfferingFormatCode &&
                    o.OfferingPlatformCode == _command.OfferingPlatformCode &&
                    o.OfferingStatusCode == _command.OfferingStatusCode &&
                    o.OfferingEdition == _command.OfferingEdition &&
                    o.AddedBy == "ProductAuthority";
            });
        }

        [Fact]
        public async Task Throw_Validation_Error_If_Product_Doesnt_ExistAsync()
        {
            _command = new AddOfferingCommand(_products.First().ProductKey + "nope", _products.First().EffectiveStartDate, Any.String(), Any.String(), Any.String(), Any.String());
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

            context.SaveChanges();
            return context;
        }
    }
}
