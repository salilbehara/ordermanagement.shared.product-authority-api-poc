using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_api.Application.Queries.Offerings;
using ordermanagement.shared.product_authority_api.Application.Queries.Products;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ordermanagement.shared.product_authority_api.test.Queries.Offerings
{
    public class GetOfferingBasedOnOfferingKeyQuery_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private List<OfferingFormatEntity> _offeringFormats;
        private IRequestHandler<GetOfferingBasedOnOfferingKeyQuery, GetOfferingBasedOnOfferingKeyQueryDto> _handler;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private List<ProductEntity> _products;
        private List<OfferingEntity> _offerings;

        public GetOfferingBasedOnOfferingKeyQuery_should()
        {
            _products = new List<ProductEntity>(new[]
            {
                Any.ProductEntity(Any.String(),new DateTime(2019, 02, 01))
            });
            _offerings = new List<OfferingEntity>(new[]
            {
                Any.OfferingEntity("key", _products.First().ProductId, _products.First().EffectiveStartDate),
                Any.OfferingEntity("key2", _products.First().ProductId, _products.First().EffectiveStartDate)
            }); ;
            _context = GetContextWithData();
            _handler = new GetOfferingBasedOnOfferingKeyQueryHandler(_context);

        }

        [Fact]
        public async void Get_Offering_By_Offering_Key()
        {
            var query = new GetOfferingBasedOnOfferingKeyQuery(_offerings.First().OfferingKey);
            var response = await _handler.Handle(query, default);
            Assert.NotEmpty(response.Offerings);
            Assert.All(response.Offerings, o => Assert.Equal(o.OfferingKey,_offerings.First().OfferingKey));
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
                        
            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);


            context.ProductTypes.AddRange(_products.Select(x => x.ProductType));
            context.ProductStatuses.AddRange(_products.Select(x => x.ProductStatus));
            context.Products.AddRange(_products);
            context.Offerings.AddRange(_offerings);

            context.SaveChanges();
            return context;
        }
    }
}
