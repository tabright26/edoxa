// Filename: ParticipantsControllerTest.cs
// Date Created: 2019-03-18
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Controllers;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.DTO;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Api.Tests.Controllers
{
    [TestClass]
    public sealed class ParticipantsControllerTest
    {
        private Mock<ILogger<ParticipantsController>> _logger;
        private Mock<IParticipantQueries> _queries;
        private Mock<IMediator> _mediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<ParticipantsController>>();
            _queries = new Mock<IParticipantQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindParticipantAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            _queries.Setup(queries => queries.FindParticipantAsync(It.IsAny<ParticipantId>())).ReturnsAsync(new ParticipantDTO()).Verifiable();

            var controller = new ParticipantsController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindParticipantAsync(It.IsAny<ParticipantId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindParticipantAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindParticipantAsync(It.IsAny<ParticipantId>())).ReturnsAsync((ParticipantDTO) null).Verifiable();

            var controller = new ParticipantsController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindParticipantAsync(It.IsAny<ParticipantId>());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindParticipantAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindParticipantAsync(It.IsAny<ParticipantId>())).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new ParticipantsController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindParticipantAsync(It.IsAny<ParticipantId>());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _logger.Verify();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }
    }
}