// Filename: MatchesControllerTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Controllers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Functional;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Api.Tests.Controllers
{
    [TestClass]
    public sealed class MatchesControllerTest
    {
        private Mock<IMediator> _mediator;
        private Mock<IMatchQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _queries = new Mock<IMatchQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindMatchAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            _queries.Setup(queries => queries.FindMatchAsync(It.IsAny<MatchId>())).ReturnsAsync(new Option<MatchDTO>(new MatchDTO())).Verifiable();

            var controller = new MatchesController(_queries.Object);

            // Act
            var result = await controller.FindMatchAsync(It.IsAny<MatchId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindMatchAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindMatchAsync(It.IsAny<MatchId>())).ReturnsAsync(new Option<MatchDTO>()).Verifiable();

            var controller = new MatchesController(_queries.Object);

            // Act
            var result = await controller.FindMatchAsync(It.IsAny<MatchId>());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }
    }
}