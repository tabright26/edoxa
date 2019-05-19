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

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Commands.Extensions;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class DepositTokenHandlerTest
    {
        private Mock<ICashierHttpContext> _mockCashierHttpContext;
        private Mock<ITokenAccountService> _mockTokenAccountService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockTokenAccountService = new Mock<ITokenAccountService>();
            _mockCashierHttpContext = new Mock<ICashierHttpContext>();
            _mockCashierHttpContext.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DepositTokenCommandHandler>.For(typeof(ICashierHttpContext), typeof(ITokenAccountService))
                                                        .WithName("DepositTokenCommandHandler")
                                                        .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_DepositTokenCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var command = new DepositTokenCommand(TokenDepositBundleType.FiftyThousand);

            _mockTokenAccountService
                .Setup(mock => mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<TokenBundle>(), It.IsAny<StripeCustomerId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TransactionStatus.Completed)
                .Verifiable();

            var handler = new DepositTokenCommandHandler(_mockCashierHttpContext.Object, _mockTokenAccountService.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            result.Should().BeOfType<Either<ValidationError, TransactionStatus>>();

            _mockTokenAccountService.Verify(
                mock => mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<TokenBundle>(), It.IsAny<StripeCustomerId>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
