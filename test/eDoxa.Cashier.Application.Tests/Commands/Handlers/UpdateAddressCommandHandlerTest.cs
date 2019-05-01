// Filename: UpdateAddressCommandHandlerTest.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Functional.Maybe;
using eDoxa.Security.Services;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class UpdateAddressCommandHandlerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task Handle_FindAsNoTrackingAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var customer = _userAggregateFactory.CreateCustomer();

            var address = customer.Shipping.Address;

            var mockUserInfoService = new Mock<IUserInfoService>();

            mockUserInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            var mockUserRepository = new Mock<IUserRepository>();

            var mockCustomerService = new Mock<CustomerService>();

            mockUserRepository.Setup(repository => repository.FindAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            mockCustomerService.Setup(
                    service => service.UpdateAsync(
                        It.IsAny<string>(),
                        It.IsAny<CustomerUpdateOptions>(),
                        It.IsAny<RequestOptions>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(customer)
                .Verifiable();

            var handler = new UpdateAddressCommandHandler(mockUserInfoService.Object, mockUserRepository.Object, mockCustomerService.Object);

            // Act
            var response = await handler.Handle(new UpdateAddressCommand(address.City, address.Country, address.Line1, address.Line2, address.PostalCode, address.State), default
            );

            // Assert
            response.Should().BeEquivalentTo(new OkObjectResult(address));

            mockUserRepository.Verify(repository => repository.FindAsNoTrackingAsync(It.IsAny<UserId>()), Times.Once);

            mockCustomerService.Verify(
                service =>
                    service.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}