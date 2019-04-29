// Filename: ParticipantMatchesControllerTest.cs
// Date Created: 2019-04-21
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
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.DTO;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Functional.Maybe;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Api.Tests.Controllers
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

            _queries.Setup(queries => queries.FindParticipantMatchesAsync(It.IsAny<ParticipantId>())).ReturnsAsync(new Option<MatchListDTO>(value)).Verifiable();

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