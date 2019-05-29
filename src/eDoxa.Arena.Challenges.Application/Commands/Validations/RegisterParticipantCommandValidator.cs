// Filename: RegisterParticipantCommandValidator.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Application.Validations.Extensions;

namespace eDoxa.Arena.Challenges.Application.Commands.Validations
{
    public sealed class RegisterParticipantCommandValidator : CommandValidator<RegisterParticipantCommand>
    {
        public RegisterParticipantCommandValidator()
        {
            this.EntityId(command => command.ChallengeId);
        }
    }
}
