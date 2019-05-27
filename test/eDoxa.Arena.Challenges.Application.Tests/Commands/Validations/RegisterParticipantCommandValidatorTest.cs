using Microsoft.VisualStudio.TestTools.UnitTesting;

using eDoxa.Arena.Challenges.Application.Commands;
using eDoxa.Arena.Challenges.Application.Commands.Validations;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

using FluentAssertions;

namespace eDoxa.Arena.Challenges.Application.Tests.Commands.Validations
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
