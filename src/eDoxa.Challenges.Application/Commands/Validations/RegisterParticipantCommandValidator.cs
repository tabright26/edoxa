// Filename: RegisterParticipantCommandValidator.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Commands.Abstractions.Validations;

using FluentValidation;

namespace eDoxa.Challenges.Application.Commands.Validations
{
    internal sealed class RegisterParticipantCommandValidator : CommandValidator<RegisterParticipantCommand>
    {
        public RegisterParticipantCommandValidator()
        {
            this.RuleFor(command => command.ChallengeId).NotEmpty();
        }
    }
}