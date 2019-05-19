// Filename: StripeCardsControllerTest.cs
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
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Commands.Result;
using eDoxa.Seedwork.Domain.Validations;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class StripeCardsControllerTest
    {
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private Mock<IStripeCardQueries> _mockCardQueries;
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCardQueries = new Mock<IStripeCardQueries>();
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<StripeCardsController>.For(typeof(IStripeCardQueries), typeof(IMediator))
                                                   .WithName("StripeCardsController")
                                                   .WithAttributes(
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
                                new StripeCardListDTO
                                {
                                    Items = new List<StripeCardDTO>
                                    {
                                        new StripeCardDTO()
                                    }
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
            _mockCardQueries.Setup(mock => mock.GetCardsAsync()).ReturnsAsync(new StripeCardListDTO()).Verifiable();

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
            var sourceToken = FakeStripeFactory.CreateSourceToken();

            _mockMediator.Setup(mock => mock.Send(It.IsAny<CreateCardCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(CommandResult.Succeeded)
                         .Verifiable();

            var controller = new StripeCardsController(_mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.CreateCardAsync(new CreateCardCommand(sourceToken));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<CreateCardCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateCardAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var sourceToken = FakeStripeFactory.CreateSourceToken();

            _mockMediator.Setup(mock => mock.Send(It.IsAny<CreateCardCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(ValidationError.Empty)
                         .Verifiable();

            var controller = new StripeCardsController(_mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.CreateCardAsync(new CreateCardCommand(sourceToken));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<CreateCardCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteCardAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var cardId = FakeStripeFactory.CreateCardId();

            _mockMediator.Setup(mock => mock.Send(It.IsAny<DeleteCardCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(CommandResult.Succeeded)
                         .Verifiable();

            var controller = new StripeCardsController(_mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.DeleteCardAsync(cardId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<DeleteCardCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteCardAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var cardId = FakeStripeFactory.CreateCardId();

            _mockMediator.Setup(mock => mock.Send(It.IsAny<DeleteCardCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(ValidationError.Empty)
                         .Verifiable();

            var controller = new StripeCardsController(_mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.DeleteCardAsync(cardId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<DeleteCardCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateCardDefaultAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var cardId = FakeStripeFactory.CreateCardId();

            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<UpdateCardDefaultCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(CommandResult.Succeeded)
                         .Verifiable();

            var controller = new StripeCardsController(_mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.UpdateCardDefaultAsync(cardId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<UpdateCardDefaultCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateCardDefaultAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var cardId = FakeStripeFactory.CreateCardId();

            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<UpdateCardDefaultCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(ValidationError.Empty)
                         .Verifiable();

            var controller = new StripeCardsController(_mockCardQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.UpdateCardDefaultAsync(cardId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<UpdateCardDefaultCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
