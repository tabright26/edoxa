﻿// Filename: UsersControllerTest.cs
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
    public sealed class UsersControllerTest
    {
        private Mock<ILogger<UsersController>> _logger;
        private Mock<IChallengeQueries> _queries;
        private Mock<IMediator> _mediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<UsersController>>();
            _queries = new Mock<IChallengeQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindUserChallengeHistoryAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var value = new ChallengeListDTO
            {
                Items = new List<ChallengeDTO>
                {
                    new ChallengeDTO()
                }
            };

            _queries.Setup(queries => queries.FindUserChallengeHistoryAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<ChallengeType>(), It.IsAny<ChallengeState>()))
                    .ReturnsAsync(value)
                    .Verifiable();

            var controller = new UsersController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindUserChallengeHistoryAsync(It.IsAny<UserId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserChallengeHistoryAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUserChallengeHistoryAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<ChallengeType>(), It.IsAny<ChallengeState>()))
                    .ReturnsAsync(new ChallengeListDTO())
                    .Verifiable();

            var controller = new UsersController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindUserChallengeHistoryAsync(It.IsAny<UserId>());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserChallengeHistoryAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUserChallengeHistoryAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<ChallengeType>(), It.IsAny<ChallengeState>()))
                    .ThrowsAsync(new Exception())
                    .Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UsersController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindUserChallengeHistoryAsync(It.IsAny<UserId>());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _logger.Verify();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }
    }
}