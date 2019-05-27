// Filename: DepositMoneyCommandHandlerTest.cs
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
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
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
    public sealed class DepositMoneyCommandHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IMoneyAccountService> _mockMoneyAccountService;
        private Mock<IUserRepository> _mockUserRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMoneyAccountService = new Mock<IMoneyAccountService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.SetupClaims();
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DepositMoneyCommandHandler>.For(typeof(IHttpContextAccessor), typeof(IMoneyAccountService), typeof(IUserRepository))
                                                        .WithName("DepositMoneyCommandHandler")
                                                        .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_DepositMoneyCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var command = new DepositMoneyCommand(MoneyDepositBundleType.Ten);

            var user = FakeCashierFactory.CreateUser();

            _mockUserRepository.Setup(mock => mock.GetUserAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockMoneyAccountService
                .Setup(mock => mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<MoneyBundle>(), It.IsAny<StripeCustomerId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TransactionStatus.Completed)
                .Verifiable();

            var handler = new DepositMoneyCommandHandler(_mockHttpContextAccessor.Object, _mockMoneyAccountService.Object, _mockUserRepository.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            result.Should().BeOfType<Either<ValidationError, TransactionStatus>>();

            _mockMoneyAccountService.Verify(
                mock => mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<MoneyBundle>(), It.IsAny<StripeCustomerId>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
