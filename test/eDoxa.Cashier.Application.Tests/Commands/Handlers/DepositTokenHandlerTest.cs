// Filename: DepositTokenHandlerTest.cs
// Date Created: 2019-05-19
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
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Commands.Extensions;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Entities;
using eDoxa.Seedwork.Domain.Validations;
using eDoxa.Testing.MSTest;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class DepositTokenHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<ITokenAccountService> _mockTokenAccountService;
        private Mock<IUserRepository> _mockUserRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockTokenAccountService = new Mock<ITokenAccountService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.SetupClaims();
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DepositTokenCommandHandler>.For(typeof(IHttpContextAccessor), typeof(ITokenAccountService), typeof(IUserRepository))
                                                        .WithName("DepositTokenCommandHandler")
                                                        .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_DepositTokenCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var command = new DepositTokenCommand(TokenDepositBundleType.FiftyThousand);

            var user = FakeCashierFactory.CreateUser();

            _mockUserRepository.Setup(mock => mock.GetUserAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockTokenAccountService
                .Setup(mock => mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<TokenBundle>(), It.IsAny<StripeCustomerId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TransactionStatus.Completed)
                .Verifiable();

            var handler = new DepositTokenCommandHandler(_mockHttpContextAccessor.Object, _mockTokenAccountService.Object, _mockUserRepository.Object);

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
