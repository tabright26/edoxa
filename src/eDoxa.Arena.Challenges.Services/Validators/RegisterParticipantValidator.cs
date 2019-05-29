// Filename: RegisterParticipantValidator.cs
// Date Created: 2019-05-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Specifications;
using eDoxa.Arena.Domain;
using eDoxa.Seedwork.Domain.Common;

using FluentValidation;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Services.Validators
{
    public sealed class RegisterParticipantValidator : AbstractValidator<Challenge>
    {
        public RegisterParticipantValidator(UserId userId, [CanBeNull] ExternalAccount externalAccount)
        {
            this.RuleFor(challenge => challenge)
                .Must(new ExternalAccountIsProvidedSpecification(externalAccount).Not().IsSatisfiedBy)
                .WithMessage("This user does not provide an external account for the challenge-specific game.");

            this.RuleFor(challenge => challenge).Must(new UserIsRegisteredSpecification(userId).IsSatisfiedBy).WithMessage("The user already is registered.");

            this.RuleFor(challenge => challenge)
                .Must(new ChallengeRegisterIsAvailableSpecification().IsSatisfiedBy)
                .WithMessage("Challenge register is available.");
        }
    }
}
