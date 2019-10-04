﻿using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ordermanagement.shared.product_authority_api.test.Commands.Products
{
    public class AddProductCommandHandler_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private readonly AddProductCommandHandler _handler;
        private AddProductCommand _command;

        public AddProductCommandHandler_should()
        {
            _context = GetContext();
            _handler = new AddProductCommandHandler(_context);
            _command = new AddProductCommand(Any.String(), Any.String(), Any.Long(), Any.String(), Any.String(), Any.String(), Any.String(), Any.String(), Any.Int());

        }

        [Fact]
        public async Task Add_Product_To_Context()
        {
            await _handler.Execute(_command);
            Assert.Single(_context.Products, p =>
            {
                return
                    p.LegacyIdSpid == _command.LegacyIdSpid &&
                    p.OnlineIssn == _command.OnlineIssn &&
                    p.PrintIssn == _command.PrintIssn &&
                    p.ProductDisplayName == _command.ProductDisplayName &&
                    p.ProductName == _command.ProductName &&
                    p.ProductStatusCode == _command.ProductStatusCode &&
                    p.ProductTypeCode == _command.ProductTypeCode &&
                    p.LegacyIdSpid == _command.LegacyIdSpid &&
                    p.PublisherProductCode == _command.PublisherProductCode;
            });
        }

        private static ProductAuthorityDatabaseContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;

            var context = new ProductAuthorityDatabaseContext(options);

            return context;
        }
    }
}
