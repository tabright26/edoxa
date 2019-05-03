﻿// Filename: CreateUserCommandHandlerTest.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.Factories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class CreateUserCommandHandlerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task HandleAsync_Create_ShouldBeInvokedExactlyOneTime()
        {
            //// Arrange
            //var userId = _userAggregateFactory.CreateUserId();

            //var customer = _userAggregateFactory.CreateCustomer();

            //var mockCustomerService = new Mock<CustomerService>();

            //mockCustomerService
            //    .Setup(service => service.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(customer)
            //    .Verifiable();

            //var handler = new CreateUserCommandHandler(mockCustomerService.Object);

            //// Act
            //await handler.HandleAsync(new CreateUserCommand(userId, customer.Email));

            //// Assert
            //mockCustomerService.Verify(
            //    service => service.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
            //    Times.Once
            //);
        }
    }
}