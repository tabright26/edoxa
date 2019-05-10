// Filename: DepositTokensCommandHandlerTest.cs
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
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class DepositTokensCommandHandlerTest
    {
        private Mock<IMapper> _mockMapper;
        private Mock<ITokenAccountService> _mockTokenAccountService;
        private Mock<IUserInfoService> _mockUserInfoService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMapper = new Mock<IMapper>();
            _mockTokenAccountService = new Mock<ITokenAccountService>();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
        }

        [TestMethod]
        public async Task Handle_FindAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var command = new DepositTokensCommand(TokenBundleType.FiftyThousand);

            _mockTokenAccountService.Setup(service =>
                    service.TransactionAsync(It.IsAny<UserId>(), It.IsAny<CustomerId>(), It.IsAny<TokenBundle>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DepositTokenTransaction(new Token(50000)))
                .Verifiable();

            var handler = new DepositTokensCommandHandler(_mockUserInfoService.Object, _mockTokenAccountService.Object, _mockMapper.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            _mockTokenAccountService.Verify(
                service => service.TransactionAsync(It.IsAny<UserId>(), It.IsAny<CustomerId>(), It.IsAny<TokenBundle>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}