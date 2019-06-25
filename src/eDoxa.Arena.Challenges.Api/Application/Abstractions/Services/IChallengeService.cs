// Filename: IChallengeService.cs
// Date Created: 2019-06-24
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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Arena.Challenges.Api.Application.Abstractions.Services
{
    public interface IChallengeService
    {
        Task FakeChallengesAsync(
            int count,
            int seed,
            ChallengeGame game = null,
            ChallengeState state = null,
            Currency entryFeeCurrency = null,
            CancellationToken cancellationToken = default
        );

        Task RegisterParticipantAsync(
            ChallengeId challengeId,
            UserId userId,
            Func<ChallengeGame, GameAccountId> funcUserGameReference,
            CancellationToken cancellationToken = default
        );

        Task CloseAsync(IDateTimeProvider closedAt, CancellationToken cancellationToken = default);

        Task SynchronizeAsync(ChallengeGame game, CancellationToken cancellationToken = default);
    }
}
