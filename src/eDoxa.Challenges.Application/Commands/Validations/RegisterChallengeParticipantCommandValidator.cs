// Filename: RegisterChallengeParticipantCommandValidator.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Application.Commands.Validations;

using FluentValidation;

namespace eDoxa.Challenges.Application.Commands.Validations
{
    public class RegisterChallengeParticipantCommandValidator : CommandValidator<RegisterChallengeParticipantCommand>
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