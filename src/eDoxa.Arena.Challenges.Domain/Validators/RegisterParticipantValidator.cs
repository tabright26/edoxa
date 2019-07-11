﻿// Filename: RegisterParticipantValidator.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using FluentValidation;

namespace eDoxa.Arena.Challenges.Domain.Validators
{
    public sealed class RegisterParticipantValidator : AbstractValidator<Challenge>
    {
        public RegisterParticipantValidator(UserId userId)
        {
            this.RuleFor(challenge => challenge)
                .Must(challenge => challenge.Participants.All(participant => participant.UserId != userId))
                .WithMessage("The user already is registered.");

            this.RuleFor(challenge => challenge)
                .Must(challenge => challenge.Participants.Count < challenge.Entries)
                .WithMessage("Challenge register is not available.");
        }
    }
}
