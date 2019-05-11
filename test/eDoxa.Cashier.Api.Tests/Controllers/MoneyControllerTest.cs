// Filename: MoneyControllerTest.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Functional.Maybe;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class MoneyControllerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private Mock<IMediator> _mockMediator;
        private Mock<IMoneyAccountQueries> _mockMoneyAccountQueries;
        private Mock<IUserInfoService> _mockUserInfoService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMoneyAccountQueries = new Mock<IMoneyAccountQueries>();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
        }

        [TestMethod]
        public async Task FindUserAccountAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _mockMoneyAccountQueries.Setup(queries => queries.FindAccountAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new Option<MoneyAccountDTO>(new MoneyAccountDTO()))
                .Verifiable();

            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.FindMoneyAccountAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMoneyAccountQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserAccountAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            _mockMoneyAccountQueries.Setup(queries => queries.FindAccountAsync(It.IsAny<UserId>())).ReturnsAsync(new Option<MoneyAccountDTO>()).Verifiable();

            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.FindMoneyAccountAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _mockMoneyAccountQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task WithdrawalAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var money = FakeCashierFactory.CreateMoney();

            var command = new WithdrawalFundsCommand(WithdrawalMoneyBundleType.Fifty);

            _mockMediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new OkObjectResult(money)).Verifiable();

            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.WithdrawalMoneyAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMoneyAccountQueries.Verify();

            _mockMediator.Verify();
        }

        [TestMethod]
        public async Task AddFundsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var money = FakeCashierFactory.CreateMoney();

            var command = new AddFundsCommand(MoneyBundleType.Ten);

            _mockMediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new OkObjectResult(money)).Verifiable();

            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.DepositMoneyAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMoneyAccountQueries.Verify();

            _mockMediator.Verify();
        }
    }
}