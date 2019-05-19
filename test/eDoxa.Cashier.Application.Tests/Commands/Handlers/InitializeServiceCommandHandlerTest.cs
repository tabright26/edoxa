// Filename: InitializeServiceCommandHandlerTest.cs
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
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Commands.Extensions;
using eDoxa.ServiceBus;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class InitializeServiceCommandHandlerTest
    {
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private Mock<IIntegrationEventService> _mockIntegrationEventService;
        private Mock<IMoneyAccountService> _mockMoneyAccountService;
        private Mock<IStripeService> _mockStripeService;
        private Mock<ITokenAccountService> _mockTokenAccountService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMoneyAccountService = new Mock<IMoneyAccountService>();
            _mockTokenAccountService = new Mock<ITokenAccountService>();
            _mockIntegrationEventService = new Mock<IIntegrationEventService>();
            _mockStripeService = new Mock<IStripeService>();
            _mockStripeService.SetupMethods();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<InitializeServiceCommandHandler>.For(
                                                                 typeof(IStripeService),
                                                                 typeof(IIntegrationEventService),
                                                                 typeof(IMoneyAccountService),
                                                                 typeof(ITokenAccountService)
                                                             )
                                                             .WithName("InitializeServiceCommandHandler")
                                                             .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_InitializeServiceCommand_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = FakeCashierFactory.CreateUserId();

            var person = FakeStripeFactory.CreatePerson();

            var handler = new InitializeServiceCommandHandler(
                _mockStripeService.Object,
                _mockIntegrationEventService.Object,
                _mockMoneyAccountService.Object,
                _mockTokenAccountService.Object
            );

            // Act
            await handler.HandleAsync(
                new InitializeServiceCommand(
                    userId,
                    person.Email,
                    person.FirstName,
                    person.LastName,
                    person.Dob.Year.HasValue ? (int) person.Dob.Year.Value : 1970,
                    person.Dob.Month.HasValue ? (int) person.Dob.Month.Value : 1,
                    person.Dob.Day.HasValue ? (int) person.Dob.Day.Value : 1
                )
            );

            // Assert
            _mockStripeService.Verify(
                mock => mock.CreateCustomerAsync(It.IsAny<UserId>(), It.IsAny<StripeAccountId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
