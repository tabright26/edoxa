// Filename: RegisterChallengeParticipantCommandValidator.cs
// Date Created: 2019-04-21
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
    internal sealed class RegisterChallengeParticipantCommandValidator : CommandValidator<RegisterChallengeParticipantCommand>
    {
        public RegisterChallengeParticipantCommandValidator()
        {
            this.RuleFor(command => command.UserId).NotEmpty();

            this.RuleFor(command => command.ChallengeId).NotEmpty();

            this.RuleFor(command => command.LinkedAccount).NotNull();

            this.RuleFor(command => command.LinkedAccount).NotEmpty();
        }
    }
}