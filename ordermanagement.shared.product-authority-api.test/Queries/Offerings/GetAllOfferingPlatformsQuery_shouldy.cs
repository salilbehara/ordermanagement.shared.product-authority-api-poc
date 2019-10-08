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
    public class GetAllOfferingPlatformsQuery_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private List<OfferingPlatformEntity> _offeringPlatforms;
        private IRequestHandler<GetAllOfferingPlatformsQuery, GetAllOfferingPlatformsQueryDto> _handler;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        public GetAllOfferingPlatformsQuery_should()
        {
            _offeringPlatforms = new List<OfferingPlatformEntity>(new[]
            {
                Any.OfferingPlatformEntity()
            });
            _context = GetContextWithData();
            _handler = new GetAllOfferingPlatformsQueryHandler(_context);

        }

        [Fact]
        public async void Get_Offering_Platforms()
        {
            var query = new GetAllOfferingPlatformsQuery();
            var response = await _handler.Handle(query, default);

            Assert.Equal(_offeringPlatforms.First().OfferingPlatformCode, response.OfferingPlatforms.First().OfferingPlatformCode);
            Assert.Equal(_offeringPlatforms.First().OfferingPlatformName, response.OfferingPlatforms.First().OfferingPlatformName);
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
                        
            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);


            context.OfferingPlatforms.AddRange(_offeringPlatforms);
            
            context.SaveChanges();
            return context;
        }
    }
}
