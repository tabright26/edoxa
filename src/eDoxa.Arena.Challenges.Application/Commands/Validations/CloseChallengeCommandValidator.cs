// Filename: CompleteChallengeCommandValidator.cs
// Date Created: 2019-05-27
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
    internal sealed class CloseChallengeCommandValidator : CommandValidator<SynchronizeChallengeCommand>
    {
        public CloseChallengeCommandValidator()
        {
            this.EntityId(command => command.ChallengeId);
        }
    }
}
