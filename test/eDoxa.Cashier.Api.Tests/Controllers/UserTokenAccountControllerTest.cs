// Filename: UserTokenAccountControllerTest.cs
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
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Factories;
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
    public sealed class UserTokenAccountControllerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        private Mock<ILogger<UserTokenAccountController>> _logger;
        private Mock<ITokenAccountQueries> _queries;
        private Mock<IMediator> _mediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<UserTokenAccountController>>();
            _queries = new Mock<ITokenAccountQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task BuyTokensAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var token = _userAggregateFactory.CreateToken();

            var command = new DepositTokensCommand(TokenBundleType.FiftyThousand);

            _mediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(token).Verifiable();

            var controller = new UserTokenAccountController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.DepositTokensAsync(user.Id, command);

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

            var command = new DepositTokensCommand(TokenBundleType.FiftyThousand);

            _mediator.Setup(mediator => mediator.Send(command, default)).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UserTokenAccountController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.DepositTokensAsync(user.Id, command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.Verify();

            _mediator.Verify();
        }
    }
}