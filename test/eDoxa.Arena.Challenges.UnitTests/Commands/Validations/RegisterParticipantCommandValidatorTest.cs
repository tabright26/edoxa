// Filename: RegisterParticipantCommandValidatorTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api.Application.Commands;
using eDoxa.Arena.Challenges.Api.Application.Commands.Validations;
using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Commands.Validations
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
