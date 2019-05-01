// Filename: UserMoneyAccountControllerTest.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional.Maybe;
using eDoxa.Security.Services;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class UserMoneyAccountControllerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        private Mock<IUserInfoService> _userInfoService;
        private Mock<IMediator> _mediator;
        private Mock<IMoneyAccountQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _userInfoService = new Mock<IUserInfoService>();
            _queries = new Mock<IMoneyAccountQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindUserAccountAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _queries.Setup(queries => queries.FindAccountAsync(It.IsAny<UserId>())).ReturnsAsync(new Option<MoneyAccountDTO>(new MoneyAccountDTO())).Verifiable();

            var controller = new AccountMoneyController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindMoneyAccountAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserAccountAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _queries.Setup(queries => queries.FindAccountAsync(It.IsAny<UserId>())).ReturnsAsync(new Option<MoneyAccountDTO>()).Verifiable();

            var controller = new AccountMoneyController(_userInfoService.Object,  _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindMoneyAccountAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task WithdrawalAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var money = _userAggregateFactory.CreateMoney();

            var command = new WithdrawMoneyCommand(money);

            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _mediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new OkObjectResult(money)).Verifiable();

            var controller = new AccountMoneyController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.WithdrawMoneyAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task AddFundsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var money = _userAggregateFactory.CreateMoney();

            var command = new DepositMoneyCommand(MoneyBundleType.Ten);

            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _mediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new OkObjectResult(money)).Verifiable();

            var controller = new AccountMoneyController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.DepositMoneyAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.Verify();
        }
    }
}