// Filename: RegisterParticipantCommandValidatorTest.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Application.Commands;
using eDoxa.Arena.Challenges.Application.Commands.Validations;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Tests.Commands.Validations
{
    [TestClass]
    public sealed class RegisterParticipantCommandValidatorTest
    {
        [TestMethod]
        public void M()
        {
            var command = new RegisterParticipantCommand(new ChallengeId());

            var validator = new RegisterParticipantCommandValidator();

            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }
    }
}
