// Filename: StripeCardsControllerTest.cs
// Date Created: 2019-06-01
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

using eDoxa.Cashier.Api.Application.Abstractions;
using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Seedwork.Testing.TestConstructor;
using eDoxa.Stripe.Data.Fakers;
using eDoxa.Stripe.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Controllers
{
    [TestClass]
    public sealed class StripeCardsControllerTest
    {
        private Mock<ICardQuery> _mockCardQueries;
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCardQueries = new Mock<ICardQuery>();
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<StripeCardsController>.ForParameters(typeof(ICardQuery), typeof(IMediator))
                .WithClassName("StripeCardsController")
                .WithClassAttributes(
                    typeof(AuthorizeAttribute),
                    typeof(ApiControllerAttribute),
                    typeof(ApiVersionAttribute),
                    typeof(ProducesAttribute),
                    typeof(RouteAttribute),
                    typeof(ApiExplorerSettingsAttribute)
                )
                .Assert();
        }

        [TestMethod]
        public async Task GetCardsAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockCardQueries.Setup(queries => queries.GetCardsAsync())
                .ReturnsAsync(
                    new List<CardViewModel>
                    {
                        new CardViewModel()
                    }
                )
                .Verifiable();

            var controller = new StripeCardsController(_mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetCardsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockCardQueries.Verify(queries => queries.GetCardsAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetCardsAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            _mockCardQueries.Setup(mock => mock.GetCardsAsync()).ReturnsAsync(new List<CardViewModel>()).Verifiable();

            var controller = new StripeCardsController(_mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetCardsAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockCardQueries.Verify(mock => mock.GetCardsAsync(), Times.Once);
        }

        [TestMethod]
        public async Task CreateCardAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockMediator.Setup(mock => mock.Send(It.IsAny<CreateCardCommand>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();

            var controller = new StripeCardsController(_mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.CreateCardAsync(new CreateCardCommand("qwe23rwr2r12rqwe123qwsda241qweasd"));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<CreateCardCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteCardAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var cardFaker = new CardFaker();

            var card = cardFaker.FakeCard();

            _mockMediator.Setup(mock => mock.Send(It.IsAny<DeleteCardCommand>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();

            var controller = new StripeCardsController(_mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.DeleteCardAsync(card.ToStripeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<DeleteCardCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateCardDefaultAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var cardFaker = new CardFaker();

            var card = cardFaker.FakeCard();

            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<UpdateCardDefaultCommand>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();

            var controller = new StripeCardsController(_mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.UpdateCardDefaultAsync(card.ToStripeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<UpdateCardDefaultCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
