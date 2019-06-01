// Filename: SynchronizeChallengeCommandValidator.cs
// Date Created: 2019-05-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Application.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Application.Validations.Extensions;

namespace eDoxa.Arena.Challenges.Application.Commands.Validations
{
    public sealed class SynchronizeChallengeCommandValidator : CommandValidator<SynchronizeChallengeCommand>
    {
        public SynchronizeChallengeCommandValidator()
        {
            this.EntityId(command => command.ChallengeId);
        }
    }
}
