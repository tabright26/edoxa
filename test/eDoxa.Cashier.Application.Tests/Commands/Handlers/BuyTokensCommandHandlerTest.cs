// Filename: BuyTokensCommandHandlerTest.cs
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

using AutoMapper;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Commands.Extensions;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest;
using eDoxa.Testing.MSTest.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class BuyTokensCommandHandlerTest
    {
        private Mock<ITokenAccountService> _mockTokenAccountService;
        private Mock<IUserInfoService> _mockUserInfoService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockTokenAccountService = new Mock<ITokenAccountService>();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<BuyTokensCommandHandler>.For(typeof(IUserInfoService), typeof(ITokenAccountService))
                .WithName("BuyTokensCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_BuyTokensCommand_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var command = new BuyTokensCommand(TokenBundleType.FiftyThousand);

            _mockTokenAccountService.Setup(mock =>
                    mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<CustomerId>(), It.IsAny<TokenBundle>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DepositTokenTransaction(new Token(50000)))
                .Verifiable();

            var handler = new BuyTokensCommandHandler(_mockUserInfoService.Object, _mockTokenAccountService.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _mockTokenAccountService.Verify(
                mock => mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<CustomerId>(), It.IsAny<TokenBundle>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}