// Filename: UserAccountControllerTest.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
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
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class UserAccountControllerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        private Mock<ILogger<UserAccountController>> _logger;
        private Mock<IAccountQueries> _queries;
        private Mock<IMediator> _mediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<UserAccountController>>();
            _queries = new Mock<IAccountQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindUserAccountAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            _queries.Setup(queries => queries.FindUserAccountAsync(It.IsAny<UserId>())).ReturnsAsync(new MoneyAccountDTO()).Verifiable();

            var controller = new UserAccountController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserAccountAsync(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _logger.VerifyNoOtherCalls();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserAccountAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            _queries.Setup(queries => queries.FindUserAccountAsync(It.IsAny<UserId>())).ReturnsAsync((MoneyAccountDTO) null).Verifiable();

            var controller = new UserAccountController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserAccountAsync(user.Id);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _logger.VerifyNoOtherCalls();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserAccountAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            _queries.Setup(queries => queries.FindUserAccountAsync(It.IsAny<UserId>())).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UserAccountController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserAccountAsync(user.Id);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task WithdrawalAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var money = _userAggregateFactory.CreateMoney();

            var command = new WithdrawalCommand(money);

            _mediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(money).Verifiable();

            var controller = new UserAccountController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.WithdrawalAsync(user.Id, command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _logger.VerifyNoOtherCalls();

            _queries.Verify();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task WithdrawalAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var money = _userAggregateFactory.CreateMoney();

            var command = new WithdrawalCommand(money);

            _mediator.Setup(mediator => mediator.Send(command, default)).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UserAccountController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.WithdrawalAsync(user.Id, command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.Verify();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task AddFundsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var money = _userAggregateFactory.CreateMoney();

            var command = new AddFundsCommand(MoneyBundleType.Ten);

            _mediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(money).Verifiable();

            var controller = new UserAccountController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.AddFundsAsync(user.Id, command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _logger.VerifyNoOtherCalls();

            _queries.Verify();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task AddFundsAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var command = new AddFundsCommand(MoneyBundleType.Ten);

            _mediator.Setup(mediator => mediator.Send(command, default)).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UserAccountController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.AddFundsAsync(user.Id, command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.Verify();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task BuyTokensAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var token = _userAggregateFactory.CreateToken();

            var command = new BuyTokensCommand(TokenBundleType.FiftyThousand);

            _mediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(token).Verifiable();

            var controller = new UserAccountController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.BuyTokensAsync(user.Id, command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _logger.VerifyNoOtherCalls();

            _queries.Verify();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task BuyTokensAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var command = new BuyTokensCommand(TokenBundleType.FiftyThousand);

            _mediator.Setup(mediator => mediator.Send(command, default)).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UserAccountController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.BuyTokensAsync(user.Id, command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.Verify();

            _mediator.Verify();
        }
    }
}