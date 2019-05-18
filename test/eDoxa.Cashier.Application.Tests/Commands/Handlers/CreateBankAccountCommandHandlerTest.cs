// Filename: CreateBankAccountCommandHandlerTest.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Commands.Extensions;
using eDoxa.Functional;
using eDoxa.ServiceBus;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class CreateBankAccountCommandHandlerTest
    {
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private Mock<ICashierSecurity> _mockCashierSecurity;
        private Mock<IIntegrationEventService> _mockIntegrationEventService;
        private Mock<IStripeService> _mockStripeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockStripeService.SetupMethods();
            _mockCashierSecurity = new Mock<ICashierSecurity>();
            _mockCashierSecurity.SetupGetProperties();
            _mockIntegrationEventService = new Mock<IIntegrationEventService>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<CreateBankAccountCommandHandler>.For(typeof(ICashierSecurity), typeof(IStripeService), typeof(IIntegrationEventService))
                .WithName("CreateBankAccountCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_CreateBankAccountCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var sourceToken = FakeStripeFactory.CreateSourceToken();

            _mockCashierSecurity.Setup(mock => mock.HasStripeBankAccount()).Returns(false);

            var handler = new CreateBankAccountCommandHandler(_mockCashierSecurity.Object, _mockStripeService.Object, _mockIntegrationEventService.Object);

            // Act
            var result = await handler.HandleAsync(new CreateBankAccountCommand(sourceToken));

            // Assert
            result.Should().BeOfType<Either>();

            _mockStripeService.Verify(mock => mock.CreateBankAccountAsync(It.IsAny<StripeAccountId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}