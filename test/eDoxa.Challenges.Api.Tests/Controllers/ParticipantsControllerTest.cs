// Filename: ParticipantsControllerTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Challenges.Api.Controllers;
using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.DTO;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Functional;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Api.Tests.Controllers
{
    [TestClass]
    public sealed class ParticipantsControllerTest
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
        public async Task FindParticipantAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            _queries.Setup(queries => queries.FindParticipantAsync(It.IsAny<ParticipantId>())).ReturnsAsync(new Option<ParticipantDTO>(new ParticipantDTO()))
                .Verifiable();

            var controller = new ParticipantsController(_queries.Object);

            // Act
            var result = await controller.FindParticipantAsync(It.IsAny<ParticipantId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindParticipantAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindParticipantAsync(It.IsAny<ParticipantId>())).ReturnsAsync(new Option<ParticipantDTO>()).Verifiable();

            var controller = new ParticipantsController(_queries.Object);

            // Act
            var result = await controller.FindParticipantAsync(It.IsAny<ParticipantId>());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }
    }
}