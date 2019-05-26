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
using eDoxa.Seedwork.Application.Validators;
using eDoxa.Seedwork.Domain.Entities;

namespace eDoxa.Arena.Challenges.Services.Validators
{
    public sealed class RegisterParticipantValidator : DomainValidator<Challenge>
    {
        public RegisterParticipantValidator(UserId userId, ExternalAccount externalAccount)
        {
            this.AddRule(
                new ExternalAccountIsProvidedSpecification(externalAccount).Not(),
                "This user does not provide an external account for the challenge-specific game."
            );

            this.AddRule(new UserIsRegisteredSpecification(userId), "The user already is registered.");

            this.AddRule(new ChallengeRegisterIsAvailableSpecification(), "Challenge register is available.");
        }
    }
}
