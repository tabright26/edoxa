// Filename: UserCardsControllerTest.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional.Maybe;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class UserCardsControllerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        private Mock<IMediator> _mediator;
        private Mock<ICardQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _queries = new Mock<ICardQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindUserCardsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            _queries.Setup(queries => queries.FindUserCardsAsync(It.IsAny<UserId>())).ReturnsAsync(new Option<CardListDTO>(new CardListDTO
            {
                Items = new List<CardDTO>
                {
                    new CardDTO()
                }
            })).Verifiable();

            var controller = new UserCardsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserCardsAsync(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserCardsAsync_ShouldBeNoContentObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            _queries.Setup(queries => queries.FindUserCardsAsync(It.IsAny<UserId>())).ReturnsAsync(new Option<CardListDTO>()).Verifiable();

            var controller = new UserCardsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserCardsAsync(userId);

            // Assert
            result.Should().BeOfType<NoContentResult>();

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

            _mediator.Setup(mediator => mediator.Send(It.IsAny<CreateCardCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new OkObjectResult(new Card())).Verifiable();

            var controller = new UserCardsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.CreateCardAsync(userId, command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task FindUserCardAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var cardId = _userAggregateFactory.CreateCardId();

            _queries.Setup(queries => queries.FindUserCardAsync(It.IsAny<UserId>(), It.IsAny<CardId>())).ReturnsAsync(new Option<CardDTO>(new CardDTO())).Verifiable();

            var controller = new UserCardsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserCardAsync(userId, cardId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task DeleteCardAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var cardId = _userAggregateFactory.CreateCardId();

            _mediator.Setup(mediator => mediator.Send(It.IsAny<DeleteCardCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new OkResult()).Verifiable();

            var controller = new UserCardsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.DeleteCardAsync(userId, cardId);

            // Assert
            result.Should().BeOfType<OkResult>();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task UpdateDefaultCardAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var userId = _userAggregateFactory.CreateUserId();

            var cardId = _userAggregateFactory.CreateCardId();

            _mediator.Setup(mediator => mediator.Send(It.IsAny<UpdateDefaultCardCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new OkObjectResult(new Customer()))
                .Verifiable();

            var controller = new UserCardsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.UpdateDefaultCardAsync(userId, cardId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }
    }
}