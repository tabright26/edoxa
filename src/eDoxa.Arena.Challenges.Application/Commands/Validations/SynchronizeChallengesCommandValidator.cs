// Filename: SynchronizeChallengesCommandValidator.cs
// Date Created: 2019-06-01
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
    public sealed class SynchronizeChallengesCommandValidator : CommandValidator<SynchronizeChallengesCommand>
    {
        public SynchronizeChallengesCommandValidator()
        {
            this.EntityId(command => command.ChallengeId);
        }
    }
}
