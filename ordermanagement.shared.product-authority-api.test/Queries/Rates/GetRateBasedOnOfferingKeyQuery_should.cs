using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_api.Application.Queries.Offerings;
using ordermanagement.shared.product_authority_api.Application.Queries.Products;
using ordermanagement.shared.product_authority_api.Application.Queries.Rates;
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
    public class GetRateBasedOnOfferingKeyQuery_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private List<RateEntity> _rates;
        private IRequestHandler<GetRateBasedOnOfferingKeyQuery, GetRateBasedOnOfferingKeyQueryDto> _handler;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private List<ProductEntity> _products;
        private List<OfferingEntity> _offerings;

        public GetRateBasedOnOfferingKeyQuery_should()
        {
            _products = new List<ProductEntity>(new[]
            {
                Any.ProductEntity(Any.String(),new DateTime(2019, 02, 01))
            });
            _offerings = new List<OfferingEntity>(new[]
            {
                Any.OfferingEntity(Any.String(), _products.First().ProductId, _products.First().EffectiveStartDate),
                Any.OfferingEntity(Any.String(), _products.First().ProductId, _products.First().EffectiveStartDate)
            });
            _rates = new List<RateEntity>(new[]
            {
                Any.RateEntity(Any.String(), _products.First().ProductId, _offerings.First().OfferingId, Any.DateTime()),
                Any.RateEntity(Any.String(), _products.First().ProductId, _offerings.First().OfferingId, Any.DateTime()),
                Any.RateEntity(Any.String(), _products.First().ProductId, _offerings.Last().OfferingId, Any.DateTime()),
            });
            _context = GetContextWithData();
            _handler = new GetRateBasedOnOfferingKeyQueryHandler(_context);

        }

        [Fact]
        public async void Get_Rates_Based_On_Offering_Key()
        {
            var query = new GetRateBasedOnOfferingKeyQuery(_offerings.First().OfferingKey);
            var response = await _handler.Handle(query, default);
            var matchingRateKeys = _rates
                .Where(o => o.OfferingId == _offerings.First().OfferingId)
                .Select(x => x.RateKey);

            Assert.All(response.Rates,
                o => Assert.Contains(o.RateKey, matchingRateKeys));

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
