// Filename: UserCardsControllerTest.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels;
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

using Stripe;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class UserCardsControllerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        private Mock<ILogger<UserCardsController>> _logger;
        private Mock<ICardQueries> _queries;
        private Mock<IMediator> _mediator;
        private Mock<IUrlHelper> _urlHelper;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<UserCardsController>>();
            _queries = new Mock<ICardQueries>();
            _mediator = new Mock<IMediator>();
            _urlHelper = new Mock<IUrlHelper>();
        }

        [TestMethod]
        public async Task FindUserCardsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            _queries.Setup(queries => queries.FindUserCardsAsync(It.IsAny<UserId>())).ReturnsAsync(new CardListDTO()).Verifiable();

            var controller = new UserCardsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserCardsAsync(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _logger.VerifyNoOtherCalls();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserCardsAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            _queries.Setup(queries => queries.FindUserCardsAsync(It.IsAny<UserId>())).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UserCardsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserCardsAsync(userId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task CreateCardAsync_ShouldBeCreatedResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var cardId = _userAggregateFactory.CreateCardId();

            var command = new CreateCardCommand(cardId.ToString());

            _mediator.Setup(mediator => mediator.Send(It.IsAny<CreateCardCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Card()).Verifiable();

            _urlHelper.Setup(helper => helper.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(string.Empty).Verifiable();

            var controller = new UserCardsController(_logger.Object, _queries.Object, _mediator.Object)
            {
                Url = _urlHelper.Object
            };

            // Act
            var result = await controller.CreateCardAsync(userId, command);

            // Assert
            result.Should().BeOfType<CreatedResult>();

            _logger.VerifyNoOtherCalls();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();

            _urlHelper.Verify();
        }

        [TestMethod]
        public async Task CreateCardAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var cardId = _userAggregateFactory.CreateCardId();

            var command = new CreateCardCommand(cardId.ToString());

            _mediator.Setup(mediator => mediator.Send(It.IsAny<CreateCardCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UserCardsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.CreateCardAsync(userId, command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();

            _urlHelper.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserCardAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var cardId = _userAggregateFactory.CreateCardId();

            _queries.Setup(queries => queries.FindUserCardAsync(It.IsAny<UserId>(), It.IsAny<CardId>())).ReturnsAsync(new CardDTO()).Verifiable();

            var controller = new UserCardsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserCardAsync(userId, cardId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _logger.VerifyNoOtherCalls();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserCardAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var cardId = _userAggregateFactory.CreateCardId();

            _queries.Setup(queries => queries.FindUserCardAsync(It.IsAny<UserId>(), It.IsAny<CardId>())).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UserCardsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserCardAsync(userId, cardId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task DeleteCardAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var cardId = _userAggregateFactory.CreateCardId();

            _mediator.Setup(mediator => mediator.Send(It.IsAny<DeleteCardCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Unit()).Verifiable();

            var controller = new UserCardsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.DeleteCardAsync(userId, cardId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _logger.VerifyNoOtherCalls();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();

            _urlHelper.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task DeleteCardAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var cardId = _userAggregateFactory.CreateCardId();

            _mediator.Setup(mediator => mediator.Send(It.IsAny<DeleteCardCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception()).Verifiable();

            var controller = new UserCardsController(_logger.Object, _queries.Object, _mediator.Object);

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            // Act
            var result = await controller.DeleteCardAsync(userId, cardId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();

            _urlHelper.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task UpdateDefaultCardAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var cardId = _userAggregateFactory.CreateCardId();

            _mediator.Setup(mediator => mediator.Send(It.IsAny<UpdateDefaultCardCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new Customer())
                     .Verifiable();

            var controller = new UserCardsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.UpdateDefaultCardAsync(userId, cardId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _logger.VerifyNoOtherCalls();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task UpdateDefaultCardAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var cardId = _userAggregateFactory.CreateCardId();

            _mediator.Setup(mediator => mediator.Send(It.IsAny<UpdateDefaultCardCommand>(), It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new Exception())
                     .Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UserCardsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.UpdateDefaultCardAsync(userId, cardId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _logger.Verify();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }
    }
}