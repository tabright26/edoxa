// Filename: ChallengeParticipantsControllerTest.cs
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
using eDoxa.Challenges.Application.Commands;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
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
    public sealed class ChallengeParticipantsControllerTest
    {
        private Mock<IMediator> _mediator;
        private Mock<IParticipantQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _queries = new Mock<IParticipantQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindChallengeParticipantsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var value = new ParticipantListDTO
            {
                Items = new List<ParticipantDTO>
                {
                    new ParticipantDTO()
                }
            };

            _queries.Setup(queries => queries.FindChallengeParticipantsAsync(It.IsAny<ChallengeId>())).ReturnsAsync(new Option<ParticipantListDTO>(value))
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
                .ReturnsAsync(new Option<ParticipantListDTO>())
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
            var command = new RegisterParticipantCommand();

            _mediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new OkResult()).Verifiable();

            var controller = new ChallengeParticipantsController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.RegisterChallengeParticipantAsync(new ChallengeId(), command);

            // Assert
            result.Should().BeOfType<OkResult>();

            _queries.VerifyNoOtherCalls();

            _mediator.Verify();
        }
    }
}