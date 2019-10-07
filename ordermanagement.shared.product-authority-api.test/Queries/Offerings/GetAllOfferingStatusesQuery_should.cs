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
    public class GetAllOfferingStatusesQuery_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private List<OfferingStatusEntity> _offeringStatuses;
        private IRequestHandler<GetAllOfferingStatusesQuery, GetAllOfferingStatusesQueryDto> _handler;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        public GetAllOfferingStatusesQuery_should()
        {
            _offeringStatuses = new List<OfferingStatusEntity>(new[]
            {
                Any.OfferingStatusEntity()
            });
            _context = GetContextWithData();
            _handler = new GetAllOfferingStatusesQueryHandler(_context);

        }

        [Fact]
        public async void Get_Offering_Statuses()
        {
            var query = new GetAllOfferingStatusesQuery();
            var response = await _handler.Handle(query, default);

            Assert.Equal(_offeringStatuses.First().OfferingStatusCode, response.OfferingStatuses.First().OfferingStatusCode);
            Assert.Equal(_offeringStatuses.First().OfferingStatusName, response.OfferingStatuses.First().OfferingStatusName);
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
                        
            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);


            context.OfferingStatuses.AddRange(_offeringStatuses);
            
            context.SaveChanges();
            return context;
        }
    }
}
