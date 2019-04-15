// Filename: ChallengeParticipantsControllerTest.cs
// Date Created: 2019-03-18
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Controllers;
using eDoxa.Challenges.Application.Commands;
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
    public sealed class ChallengeParticipantsControllerTest
    {
        private Mock<ILogger<ChallengeParticipantsController>> _logger;
        private Mock<IParticipantQueries> _queries;
        private Mock<IMediator> _mediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<ChallengeParticipantsController>>();
            _queries = new Mock<IParticipantQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindChallengeParticipantsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var value = new ParticipantListDTO
            {
                Items = new List<ParticipantDTO>
                {
                    new ParticipantDTO()
                }
            };

            _queries.Setup(queries => queries.FindChallengeParticipantsAsync(It.IsAny<ChallengeId>())).ReturnsAsync(value).Verifiable();

            var controller = new ChallengeParticipantsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindChallengeParticipantsAsync(It.IsAny<ChallengeId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindChallengeParticipantsAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                    .ReturnsAsync(new ParticipantListDTO())
                    .Verifiable();

            var controller = new ChallengeParticipantsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindChallengeParticipantsAsync(It.IsAny<ChallengeId>());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindChallengeParticipantsAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindChallengeParticipantsAsync(It.IsAny<ChallengeId>())).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new ChallengeParticipantsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindChallengeParticipantsAsync(It.IsAny<ChallengeId>());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _logger.Verify();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task RegisterChallengeParticipantAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var command = new RegisterChallengeParticipantCommand(new ChallengeId(), new UserId());

            _mediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(It.IsAny<Unit>()).Verifiable();

            var controller = new ChallengeParticipantsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.RegisterChallengeParticipantAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.VerifyNoOtherCalls();
            _mediator.Verify();
        }

        [TestMethod]
        public async Task RegisterChallengeParticipantAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var command = new RegisterChallengeParticipantCommand(new ChallengeId(), new UserId());

            _mediator.Setup(mediator => mediator.Send(command, default)).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new ChallengeParticipantsController(_logger.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.RegisterChallengeParticipantAsync(command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _logger.Verify();
            _queries.VerifyNoOtherCalls();
            _mediator.Verify();
        }
    }
}