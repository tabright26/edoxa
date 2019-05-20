// Filename: ParticipantMatchesControllerTest.cs
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
    public sealed class ParticipantMatchesControllerTest
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

            _queries.Setup(queries => queries.FindParticipantMatchesAsync(It.IsAny<ParticipantId>())).ReturnsAsync(new Option<MatchListDTO>(value))
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
            _queries.Setup(queries => queries.FindParticipantMatchesAsync(It.IsAny<ParticipantId>())).ReturnsAsync(new Option<MatchListDTO>()).Verifiable();

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