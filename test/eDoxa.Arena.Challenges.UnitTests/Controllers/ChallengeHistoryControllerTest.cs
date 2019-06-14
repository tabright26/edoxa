// Filename: ChallengeHistoryControllerTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Abstractions;
using eDoxa.Arena.Challenges.Api.Controllers;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.Extensions;
using eDoxa.Seedwork.Common.Enumerations;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Controllers
{
    [TestClass]
    public sealed class ChallengeHistoryControllerTest
    {
        private Mock<IMediator> _mediator;
        private Mock<IHttpContextAccessor> _mockUserInfoService;
        private Mock<IChallengeQuery> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _queries = new Mock<IChallengeQuery>();
            _mediator = new Mock<IMediator>();
            _mockUserInfoService = new Mock<IHttpContextAccessor>();
            _mockUserInfoService.SetupClaims();
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUserChallengeHistoryAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(
                    new List<ChallengeViewModel>
                    {
                        new ChallengeViewModel()
                    }
                )
                .Verifiable();

            var controller = new ChallengeHistoryController(_queries.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUserChallengeHistoryAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(new List<ChallengeViewModel>())
                .Verifiable();

            var controller = new ChallengeHistoryController(_queries.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }
    }
}
