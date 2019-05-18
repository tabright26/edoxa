// Filename: DepositTokenHandlerTest.cs
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
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Commands.Extensions;
using eDoxa.Functional;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class DepositTokenHandlerTest
    {
        private Mock<ICashierSecurity> _mockCashierSecurity;
        private Mock<ITokenAccountService> _mockTokenAccountService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockTokenAccountService = new Mock<ITokenAccountService>();
            _mockCashierSecurity = new Mock<ICashierSecurity>();
            _mockCashierSecurity.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DepositTokenCommandHandler>.For(typeof(ICashierSecurity), typeof(ITokenAccountService))
                .WithName("DepositTokenCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_DepositTokenCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var command = new DepositTokenCommand(TokenDepositBundleType.FiftyThousand);

            _mockTokenAccountService.Setup(mock =>
                    mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<StripeCustomerId>(), It.IsAny<TokenBundle>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TransactionStatus.Paid)
                .Verifiable();

            var handler = new DepositTokenCommandHandler(_mockCashierSecurity.Object, _mockTokenAccountService.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            result.Should().BeOfType<Either<TransactionStatus>>();

            _mockTokenAccountService.Verify(
                mock => mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<StripeCustomerId>(), It.IsAny<TokenBundle>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}