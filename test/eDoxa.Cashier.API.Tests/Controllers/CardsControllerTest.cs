// Filename: UserCardsControllerTest.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
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
using eDoxa.Security.Services;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class CardsControllerTest
    {
        private static readonly UserAggregateFactory UserAggregateFactory = UserAggregateFactory.Instance;

        private Mock<IUserInfoService> _userInfoService;
        private Mock<IMediator> _mediator;
        private Mock<ICardQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _userInfoService = new Mock<IUserInfoService>();
            _queries = new Mock<ICardQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindUserCardsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _userInfoService.SetupGet(userInfoService => userInfoService.CustomerId).Returns(new Option<string>("cus_123qweqwe"));

            _queries.Setup(queries => queries.FindUserCardsAsync(It.IsAny<CustomerId>())).ReturnsAsync(new Option<CardListDTO>(new CardListDTO
            {
                Items = new List<CardDTO>
                {
                    new CardDTO()
                }
            })).Verifiable();

            var controller = new CardsController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserCardsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserCardsAsync_ShouldBeNoContentObjectResult()
        {
            // Arrange
            _userInfoService.SetupGet(userInfoService => userInfoService.CustomerId).Returns(new Option<string>("cus_123qweqwe"));

            _queries.Setup(queries => queries.FindUserCardsAsync(It.IsAny<CustomerId>())).ReturnsAsync(new Option<CardListDTO>()).Verifiable();

            var controller = new CardsController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserCardsAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task CreateCardAsync_ShouldBeCreatedResult()
        {
            // Arrange
            var cardId = UserAggregateFactory.CreateCardId();

            var command = new CreateCardCommand(cardId.ToString());

            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _mediator.Setup(mediator => mediator.Send(It.IsAny<CreateCardCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new OkObjectResult(new Card())).Verifiable();

            var controller = new CardsController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.CreateCardAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task FindUserCardAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var cardId = UserAggregateFactory.CreateCardId();

            _userInfoService.SetupGet(userInfoService => userInfoService.CustomerId).Returns(new Option<string>("cus_123qweqwe"));

            _queries.Setup(queries => queries.FindUserCardAsync(It.IsAny<CustomerId>(), It.IsAny<CardId>())).ReturnsAsync(new Option<CardDTO>(new CardDTO())).Verifiable();

            var controller = new CardsController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindUserCardAsync(cardId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task DeleteCardAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var cardId = UserAggregateFactory.CreateCardId();

            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _mediator.Setup(mediator => mediator.Send(It.IsAny<DeleteCardCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new OkResult()).Verifiable();

            var controller = new CardsController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.DeleteCardAsync( cardId);

            // Assert
            result.Should().BeOfType<OkResult>();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }

        [TestMethod]
        public async Task UpdateDefaultCardAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var cardId = UserAggregateFactory.CreateCardId();

            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _mediator.Setup(mediator => mediator.Send(It.IsAny<UpdateDefaultCardCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new OkObjectResult(new Customer()))
                .Verifiable();

            var controller = new CardsController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.UpdateDefaultCardAsync(cardId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }
    }
}