// Filename: ChallengeParticipantsControllerTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Controllers;
using eDoxa.Arena.Challenges.Application.Commands;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.DTO.Queries;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Tests.Controllers
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
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindChallengeParticipantsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(new List<ParticipantDTO>
                {
                    new ParticipantDTO()
                })
                .Verifiable();

            var controller = new ChallengeParticipantsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindChallengeParticipantsAsync(It.IsAny<ChallengeId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindChallengeParticipantsAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(new List<ParticipantDTO>())
                .Verifiable();

            var controller = new ChallengeParticipantsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.FindChallengeParticipantsAsync(It.IsAny<ChallengeId>());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _queries.Verify();
            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task RegisterChallengeParticipantAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _mediator.Setup(mediator => mediator.Send(It.IsAny<RegisterParticipantCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ParticipantDTO())
                .Verifiable();

            var controller = new ChallengeParticipantsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.RegisterChallengeParticipantAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }
    }
}
