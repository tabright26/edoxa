// Filename: CreateUserCommandHandlerTest.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Commands.Extensions;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class CreateUserCommandHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private Mock<IStripeService> _mockStripeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockStripeService.SetupMethods();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<CreateUserCommandHandler>.For(typeof(IStripeService))
                .WithName("CreateUserCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_CreateUserCommand_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var userId = FakeCashierFactory.CreateUserId();

            var customer = FakeCashierFactory.CreateCustomer();

            //mockCustomerService
            //    .Setup(service => service.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(customer)
            //    .Verifiable();

            var handler = new CreateUserCommandHandler(_mockStripeService.Object);

            // Act
            await handler.HandleAsync(new CreateUserCommand(userId, customer.Email));

            //mockCustomerService.Verify(
            //    service => service.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
            //    Times.Once
            //);

            // Assert
            _mockStripeService.Verify(mock => mock.CreateCustomerAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}