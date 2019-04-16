// Filename: UsersControllerTest.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;
using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Testing.MSTest.Extensions;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stripe;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        private Mock<ILogger<UsersController>> _logger;
        private Mock<IMediator> _mediator;
        private Mock<IAddressQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<UsersController>>();
            _queries = new Mock<IAddressQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindUserAddressAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            _queries.Setup(queries => queries.FindUserAddressAsync(It.IsAny<UserId>())).ReturnsAsync(new AddressDTO())
                .Verifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserAddressAsync(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _logger.VerifyNoOtherCalls();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserAddressAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            _queries.Setup(queries => queries.FindUserAddressAsync(It.IsAny<UserId>())).ThrowsAsync(new Exception())
                .Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserAddressAsync(userId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task UpdateAddressAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var customer = _userAggregateFactory.CreateCustomer();

            var address = customer.Shipping.Address;

            var command = new UpdateAddressCommand(userId, address.City, address.Country, address.Line1, address.Line2,
                address.PostalCode, address.State);

            _mediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new Address()).Verifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.UpdateAddressAsync(userId, command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _logger.VerifyNoOtherCalls();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task UpdateAddressAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var customer = _userAggregateFactory.CreateCustomer();

            var address = customer.Shipping.Address;

            var command = new UpdateAddressCommand(userId, address.City, address.Country, address.Line1, address.Line2,
                address.PostalCode, address.State);

            _mediator.Setup(mediator => mediator.Send(command, default)).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.UpdateAddressAsync(userId, command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }
    }
}