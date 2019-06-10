// Filename: WithdrawCommandHandlerTest.cs
// Date Created: 2019-06-01
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

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Api.Application.Commands.Handlers;
using eDoxa.Cashier.Api.Application.Data.Fakers;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.Abstractions.Services;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Transactions;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.UnitTests.Extensions;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Testing.TestConstructor;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Commands.Handlers
{
    [TestClass]
    public sealed class WithdrawCommandHandlerTest
    {
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IAccountService> _mockMoneyAccountService;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IMapper> _mockMapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMoneyAccountService = new Mock<IAccountService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.SetupClaims();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<WithdrawCommandHandler>.ForParameters(typeof(IHttpContextAccessor), typeof(IAccountService), typeof(IUserRepository), typeof(IMapper))
                .WithClassName("WithdrawCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_WithdrawMoneyCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var command = new WithdrawCommand(Money.Fifty.Amount);

            var userFaker = new UserFaker();

            var user = userFaker.FakeNewUser();

            _mockUserRepository.Setup(mock => mock.GetUserAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockMoneyAccountService.Setup(mock => mock.WithdrawAsync(It.IsAny<UserId>(), It.IsAny<Money>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new MoneyWithdrawTransaction(Money.Ten))
                .Verifiable();

            _mockMapper.Setup(x => x.Map<TransactionViewModel>(It.IsAny<ITransaction>())).Returns(new TransactionViewModel()).Verifiable();

            var handler = new WithdrawCommandHandler(
                _mockHttpContextAccessor.Object,
                _mockMoneyAccountService.Object,
                _mockUserRepository.Object,
                _mockMapper.Object
            );

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            result.Should().BeOfType<TransactionViewModel>();

            _mockMoneyAccountService.Verify(mock => mock.WithdrawAsync(It.IsAny<UserId>(), It.IsAny<Money>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
