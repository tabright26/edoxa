// Filename: ChallengeParticipantsControllerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Commands;
using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Controllers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Controllers
{
    [TestClass]
    public sealed class ChallengeParticipantsControllerTest
    {
        private Mock<IMediator> _mediator;
        private Mock<IParticipantQuery> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _queries = new Mock<IParticipantQuery>();
            _queries.SetupGet(matchQuery => matchQuery.Mapper).Returns(MapperExtensions.Mapper);
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(25392992);
            var challenge = challengeFaker.Generate();

            _queries.Setup(queries => queries.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challenge.Participants)
                .Verifiable();

            var controller = new ChallengeParticipantsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.GetAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(new Collection<Participant>())
                .Verifiable();

            var controller = new ChallengeParticipantsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.GetAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _mediator.Setup(mediator => mediator.Send(It.IsAny<RegisterParticipantCommand>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();

            var controller = new ChallengeParticipantsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.PostAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }
    }
}
