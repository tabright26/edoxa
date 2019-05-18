// Filename: VerifyAccountCommandHandlerTest.cs
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
using eDoxa.Testing.MSTest;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class VerifyAccountCommandHandlerTest
    {
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private Mock<ICashierSecurity> _mockCashierSecurity;
        private Mock<IStripeService> _mockStripeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockCashierSecurity = new Mock<ICashierSecurity>();
            _mockCashierSecurity.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<VerifyAccountCommandHandler>.For(typeof(IStripeService), typeof(ICashierSecurity))
                .WithName("VerifyAccountCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_VerifyAccountCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var address = FakeStripeFactory.CreateAddress();

            var command = new VerifyAccountCommand(address.Line1, address.Line2, address.City, address.State, address.PostalCode, true);

            _mockStripeService.Setup(mock => mock.VerifyAccountAsync(It.IsAny<StripeAccountId>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new VerifyAccountCommandHandler(_mockStripeService.Object, _mockCashierSecurity.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            result.Should().BeOfType<Either>();

            _mockStripeService.Verify(
                mock => mock.VerifyAccountAsync(It.IsAny<StripeAccountId>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}