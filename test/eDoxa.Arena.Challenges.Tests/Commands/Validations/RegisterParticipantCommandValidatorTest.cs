// Filename: RegisterParticipantCommandValidatorTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Application.Commands;
using eDoxa.Arena.Challenges.Application.Commands.Validations;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Tests.Utilities.Mocks.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Tests.Commands.Validations
{
    [TestClass]
    public sealed class RegisterParticipantCommandValidatorTest
    {
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IChallengeRepository> _mockChallengeRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.SetupClaims();
            _mockChallengeRepository = new Mock<IChallengeRepository>();
        }

        [TestMethod]
        public void M()
        {
            var command = new RegisterParticipantCommand(new ChallengeId());

            var validator = new RegisterParticipantCommandValidator(_mockHttpContextAccessor.Object, _mockChallengeRepository.Object);

            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }
    }
}
