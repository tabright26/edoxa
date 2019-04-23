// Filename: ChallengesControllerTest.cs
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
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.DTO;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Seedwork.Domain.Common.Enums;
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
    public sealed class ChallengesControllerTest
    {
        private Mock<ILogger<ChallengesController>> _logger;
        private Mock<IChallengeQueries> _queries;
        private Mock<IMediator> _mediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<ChallengesController>>();
            _queries = new Mock<IChallengeQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindChallengesAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var value = new ChallengeListDTO
            {
                Items = new List<ChallengeDTO>
                {
                    new ChallengeDTO()
                }
            };
            

            _queries.Setup(queries => queries.FindChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeType>(), It.IsAny<ChallengeState1>()))
                    .ReturnsAsync(value)
                    .Verifiable();

            var controller = new ChallengesController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindChallengesAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindChallengesAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeType>(), It.IsAny<ChallengeState1>()))
                    .ReturnsAsync(new ChallengeListDTO())
                    .Verifiable();

            var controller = new ChallengesController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindChallengesAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindChallengesAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeType>(), It.IsAny<ChallengeState1>()))
                    .ThrowsAsync(new Exception())
                    .Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new ChallengesController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindChallengesAsync();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _logger.Verify();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindChallengeAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            _queries.Setup(queries => queries.FindChallengeAsync(It.IsAny<ChallengeId>()))
                    .ReturnsAsync(new ChallengeDTO())
                    .Verifiable();

            var controller = new ChallengesController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindChallengeAsync(It.IsAny<ChallengeId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindChallengeAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindChallengeAsync(It.IsAny<ChallengeId>()))
                    .ReturnsAsync((ChallengeDTO) null)
                    .Verifiable();

            var controller = new ChallengesController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindChallengeAsync(It.IsAny<ChallengeId>());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindChallengeAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindChallengeAsync(It.IsAny<ChallengeId>()))
                    .ThrowsAsync(new Exception())
                    .Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new ChallengesController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindChallengeAsync(It.IsAny<ChallengeId>());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _logger.Verify();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }
    }
}