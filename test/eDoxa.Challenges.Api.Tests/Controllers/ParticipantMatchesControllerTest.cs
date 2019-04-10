// Filename: ParticipantMatchesControllerTest.cs
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
    public sealed class ParticipantMatchesControllerTest
    {
        private Mock<ILogger<ParticipantMatchesController>> _logger;
        private Mock<IMatchQueries> _queries;
        private Mock<IMediator> _mediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<ParticipantMatchesController>>();
            _queries = new Mock<IMatchQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindParticipantMatchesAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var value = new MatchListDTO
            {
                Items = new List<MatchDTO>
                {
                    new MatchDTO()
                }
            };

            _queries.Setup(queries => queries.FindParticipantMatchesAsync(It.IsAny<ParticipantId>())).ReturnsAsync(value).Verifiable();

            var controller = new ParticipantMatchesController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindParticipantMatchesAsync(It.IsAny<ParticipantId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindParticipantMatchesAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindParticipantMatchesAsync(It.IsAny<ParticipantId>())).ReturnsAsync(new MatchListDTO()).Verifiable();

            var controller = new ParticipantMatchesController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindParticipantMatchesAsync(It.IsAny<ParticipantId>());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindParticipantMatchesAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindParticipantMatchesAsync(It.IsAny<ParticipantId>())).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new ParticipantMatchesController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindParticipantMatchesAsync(It.IsAny<ParticipantId>());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _logger.Verify();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }
    }
}