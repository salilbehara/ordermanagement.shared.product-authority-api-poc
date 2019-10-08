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
    public class GetAllOfferingFormatsQuery_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private List<OfferingFormatEntity> _offeringFormats;
        private IRequestHandler<GetAllOfferingFormatsQuery, GetAllOfferingFormatsQueryDto> _handler;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        public GetAllOfferingFormatsQuery_should()
        {
            _offeringFormats = new List<OfferingFormatEntity>(new[]
            {
                Any.OfferingFormatEntity()
            });
            _context = GetContextWithData();
            _handler = new GetAllOfferingFormatsQueryHandler(_context);

        }

        [Fact]
        public async void Get_Offering_Formats()
        {
            var query = new GetAllOfferingFormatsQuery();
            var response = await _handler.Handle(query, default);

            Assert.Equal(_offeringFormats.First().OfferingFormatCode, response.OfferingFormats.First().OfferingFormatCode);
            Assert.Equal(_offeringFormats.First().OfferingFormatName, response.OfferingFormats.First().OfferingFormatName);
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
                        
            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);


            context.OfferingFormats.AddRange(_offeringFormats);
            
            context.SaveChanges();
            return context;
        }
    }
}
