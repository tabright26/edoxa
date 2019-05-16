﻿// Filename: CardsControllerTest.cs
// Date Created: 2019-05-06
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
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Functional;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class CardsControllerTest
    {
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private Mock<ICardQueries> _mockCardQueries;
        private Mock<IMediator> _mockMediator;
        private Mock<IUserInfoService> _mockUserInfoService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCardQueries = new Mock<ICardQueries>();
            _mockMediator = new Mock<IMediator>();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<CardsController>.For(typeof(IUserInfoService), typeof(ICardQueries), typeof(IMediator))
                .WithName("CardsController")
                .WithAttributes(typeof(AuthorizeAttribute), typeof(ApiControllerAttribute), typeof(ApiVersionAttribute), typeof(ProducesAttribute),
                    typeof(RouteAttribute))
                .Assert();
        }

        [TestMethod]
        public async Task GetCardsAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockCardQueries.Setup(queries => queries.GetCardsAsync(It.IsAny<StripeCustomerId>())).ReturnsAsync(new Option<CardListDTO>(new CardListDTO
            {
                Items = new List<CardDTO>
                {
                    new CardDTO()
                }
            })).Verifiable();

            var controller = new CardsController(_mockUserInfoService.Object, _mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetCardsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockCardQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetCardsAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            _mockCardQueries.Setup(queries => queries.GetCardsAsync(It.IsAny<StripeCustomerId>())).ReturnsAsync(new Option<CardListDTO>()).Verifiable();

            var controller = new CardsController(_mockUserInfoService.Object, _mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetCardsAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockCardQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task CreateCardAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var cardId = FakeStripeFactory.CreateCardId();

            var command = new CreateCardCommand(cardId.ToString());

            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<CreateCardCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new OkObjectResult(new Card())).Verifiable();

            var controller = new CardsController(_mockUserInfoService.Object, _mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.CreateCardAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockCardQueries.VerifyNoOtherCalls();

            _mockMediator.Verify();
        }

        [TestMethod]
        public async Task GetCardAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var cardId = FakeStripeFactory.CreateCardId();

            _mockCardQueries.Setup(queries => queries.GetCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>()))
                .ReturnsAsync(new Option<CardDTO>(new CardDTO()))
                .Verifiable();

            var controller = new CardsController(_mockUserInfoService.Object, _mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetCardAsync(cardId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockCardQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task DeleteCardAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var cardId = FakeStripeFactory.CreateCardId();

            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DeleteCardCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new OkResult())
                .Verifiable();

            var controller = new CardsController(_mockUserInfoService.Object, _mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.DeleteCardAsync(cardId);

            // Assert
            result.Should().BeOfType<OkResult>();

            _mockCardQueries.VerifyNoOtherCalls();

            _mockMediator.Verify();
        }

        [TestMethod]
        public async Task UpdateCardDefaultAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var cardId = FakeStripeFactory.CreateCardId();

            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<UpdateCardDefaultCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new OkObjectResult(new Customer()))
                .Verifiable();

            var controller = new CardsController(_mockUserInfoService.Object, _mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.UpdateCardDefaultAsync(cardId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockCardQueries.VerifyNoOtherCalls();

            _mockMediator.Verify();
        }
    }
}