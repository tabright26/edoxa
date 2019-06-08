// Filename: ParticipantMatchesControllerTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Controllers;
using eDoxa.Arena.Challenges.Application.Abstractions.Queries;
using eDoxa.Arena.Challenges.Application.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Tests.Controllers
{
    [TestClass]
    public sealed class ParticipantMatchesControllerTest
    {
        private Mock<IMediator> _mediator;
        private Mock<IMatchQuery> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _queries = new Mock<IMatchQuery>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindParticipantMatchesAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindParticipantMatchesAsync(It.IsAny<ParticipantId>()))
                .ReturnsAsync(new List<MatchViewModel>
                {
                    new MatchViewModel()
                })
                .Verifiable();

            var controller = new ParticipantMatchesController(_queries.Object);

            // Act
            var result = await controller.FindParticipantMatchesAsync(It.IsAny<ParticipantId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindParticipantMatchesAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindParticipantMatchesAsync(It.IsAny<ParticipantId>())).ReturnsAsync(new List<MatchViewModel>()).Verifiable();

            var controller = new ParticipantMatchesController(_queries.Object);

            // Act
            var result = await controller.FindParticipantMatchesAsync(It.IsAny<ParticipantId>());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }
    }
}
