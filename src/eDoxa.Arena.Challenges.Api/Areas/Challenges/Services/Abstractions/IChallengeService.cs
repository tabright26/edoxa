// Filename: IChallengeService.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Services.Abstractions
{
    public interface IChallengeService
    {
        Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId);

        Task<ValidationResult> RegisterParticipantAsync(
            IChallenge challenge,
            UserId userId,
            PlayerId playerId,
            IDateTimeProvider registeredAt,
            CancellationToken cancellationToken = default
        );

        Task SynchronizeAsync(Game game, TimeSpan interval, IDateTimeProvider synchronizedAt, CancellationToken cancellationToken = default);
    }
}
