// Filename: DepositMoneyCommandHandlerTest.cs
// Date Created: 2019-05-29
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
using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Transactions;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.Services.Abstractions;
using eDoxa.Cashier.Tests.Utilities.Fakes;
using eDoxa.Cashier.Tests.Utilities.Mocks.Extensions;
using eDoxa.Commands.Extensions;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Abstactions;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class DepositCommandHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
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
            ConstructorTests<DepositCommandHandler>.For(typeof(IHttpContextAccessor), typeof(IAccountService), typeof(IUserRepository), typeof(IMapper))
                .WithName("DepositCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_DepositMoneyCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var command = new DepositCommand(10, CurrencyType.Money);

            var user = FakeCashierFactory.CreateUser();

            _mockUserRepository.Setup(mock => mock.GetUserAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockMoneyAccountService
                .Setup(mock => mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<ICurrency>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new MoneyDepositTransaction(Money.Ten))
                .Verifiable();

            _mockMapper.Setup(x => x.Map<TransactionDTO>(It.IsAny<ITransaction>())).Returns(new TransactionDTO()).Verifiable();

            var handler = new DepositCommandHandler(_mockHttpContextAccessor.Object, _mockMoneyAccountService.Object, _mockUserRepository.Object, _mockMapper.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            result.Should().BeOfType<TransactionDTO>();

            _mockMoneyAccountService.Verify(
                mock => mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<ICurrency>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
