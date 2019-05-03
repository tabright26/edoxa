// Filename: UsersControllerTest.cs
// Date Created: 2019-04-21
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
using eDoxa.Functional.Maybe;
using eDoxa.Security.Services;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        private Mock<IUserInfoService> _userInfoService;
        private Mock<IMediator> _mediator;
        private Mock<IAddressQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _userInfoService = new Mock<IUserInfoService>();
            _queries = new Mock<IAddressQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindUserAddressAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _userInfoService.SetupGet(userInfoService => userInfoService.CustomerId).Returns(new Option<string>("cus_123qweqwe"));

            _queries.Setup(queries => queries.FindUserAddressAsync(It.IsAny<CustomerId>())).ReturnsAsync(new Option<AddressDTO>(new AddressDTO()))
                .Verifiable();

            var controller = new AddressController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserAddressAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task UpdateAddressAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var customer = _userAggregateFactory.CreateCustomer();

            var address = customer.Shipping.Address;

            var command = new UpdateAddressCommand(address.City, address.Country, address.Line1, address.Line2,
                address.PostalCode, address.State);

            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _mediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new OkObjectResult(new Address())).Verifiable();

            var controller = new AddressController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.UpdateAddressAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }
    }
}