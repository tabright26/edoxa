// Filename: ChallengeHistoryControllerTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Controllers;
using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.DTO;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Functional.Maybe;
using eDoxa.Security.Abstractions;
using eDoxa.Seedwork.Enumerations;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Api.Tests.Controllers
{
    [TestClass]
    public sealed class ChallengeHistoryControllerTest
    {
        private Mock<IMediator> _mediator;
        private Mock<IUserInfoService> _mockUserInfoService;
        private Mock<IChallengeQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _queries = new Mock<IChallengeQueries>();
            _mediator = new Mock<IMediator>();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
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

            _queries.Setup(queries =>
                    queries.FindUserChallengeHistoryAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<ChallengeType>(), It.IsAny<ChallengeState1>()))
                .ReturnsAsync(new Option<ChallengeListDTO>(value))
                .Verifiable();

            var controller = new ChallengeHistoryController(_mockUserInfoService.Object, _queries.Object);

            // Act
            var result = await controller.FindUserChallengeHistoryAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserChallengeHistoryAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries =>
                    queries.FindUserChallengeHistoryAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<ChallengeType>(), It.IsAny<ChallengeState1>()))
                .ReturnsAsync(new Option<ChallengeListDTO>())
                .Verifiable();

            var controller = new ChallengeHistoryController(_mockUserInfoService.Object, _queries.Object);

            // Act
            var result = await controller.FindUserChallengeHistoryAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }
    }
}