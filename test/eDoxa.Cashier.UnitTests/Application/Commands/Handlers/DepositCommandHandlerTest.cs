// Filename: DepositCommandHandlerTest.cs
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
using eDoxa.Cashier.Api.Application.Fakers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Transactions;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.ViewModels;
using eDoxa.Cashier.UnitTests.Helpers.Mocks;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Testing.TestConstructor;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Application.Commands.Handlers
{
    [TestClass]
    public sealed class DepositCommandHandlerTest
    {
        private MockHttpContextAccessor _mockHttpContextAccessor;
        private Mock<IAccountService> _mockMoneyAccountService;
        private Mock<IUserQuery> _mockUserQuery;
        private Mock<IMapper> _mockMapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpContextAccessor = new MockHttpContextAccessor();
            _mockMoneyAccountService = new Mock<IAccountService>();
            _mockUserQuery = new Mock<IUserQuery>();
            _mockMapper = new Mock<IMapper>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<DepositCommandHandler>.ForParameters(typeof(IHttpContextAccessor), typeof(IAccountService), typeof(IUserQuery), typeof(IMapper))
                .WithClassName("DepositCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_DepositMoneyCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var command = new DepositCommand(10, CurrencyType.Money);

            var userFaker = new UserFaker();

            var user = userFaker.FakeNewUser();

            _mockUserQuery.Setup(userQuery => userQuery.FindUserAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockMoneyAccountService.Setup(accountService => accountService.DepositAsync(It.IsAny<UserId>(), It.IsAny<ICurrency>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new MoneyDepositTransaction(Money.Ten))
                .Verifiable();

            _mockMapper.Setup(mapper => mapper.Map<TransactionViewModel>(It.IsAny<ITransaction>())).Returns(new TransactionViewModel()).Verifiable();

            var handler = new DepositCommandHandler(
                _mockHttpContextAccessor.Object,
                _mockMoneyAccountService.Object,
                _mockUserQuery.Object,
                _mockMapper.Object
            );

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            result.Should().BeOfType<TransactionViewModel>();

            _mockMoneyAccountService.Verify(accountService => accountService.DepositAsync(It.IsAny<UserId>(), It.IsAny<ICurrency>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
