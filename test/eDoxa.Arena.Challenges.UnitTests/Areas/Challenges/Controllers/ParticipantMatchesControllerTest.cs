// Filename: ParticipantMatchesControllerTest.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Controllers
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
            _queries.SetupGet(matchQuery => matchQuery.Mapper).Returns(MapperExtensions.Mapper);
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.InProgress);
            challengeFaker.UseSeed(95632852);
            var challenge = challengeFaker.Generate();
            var participants = challenge.Participants;
            var participant = participants.First();
            var matches = participant.Matches;

            _queries.Setup(queries => queries.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>())).ReturnsAsync(matches).Verifiable();

            var controller = new ParticipantMatchesController(_queries.Object);

            // Act
            var result = await controller.GetAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>())).ReturnsAsync(new Collection<IMatch>()).Verifiable();

            var controller = new ParticipantMatchesController(_queries.Object);

            // Act
            var result = await controller.GetAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }
    }
}
