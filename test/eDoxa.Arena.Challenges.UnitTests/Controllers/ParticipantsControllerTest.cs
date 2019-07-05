// Filename: ParticipantsControllerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Controllers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Domain.ViewModels;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Controllers
{
    [TestClass]
    public sealed class ParticipantsControllerTest
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
        public async Task GetByIdAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            _queries.Setup(queries => queries.FindParticipantAsync(It.IsAny<ParticipantId>())).ReturnsAsync(new ParticipantViewModel()).Verifiable();

            var controller = new ParticipantsController(_queries.Object);

            // Act
            var result = await controller.GetByIdAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindParticipantAsync(It.IsAny<ParticipantId>())).ReturnsAsync((ParticipantViewModel) null).Verifiable();

            var controller = new ParticipantsController(_queries.Object);

            // Act
            var result = await controller.GetByIdAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }
    }
}
