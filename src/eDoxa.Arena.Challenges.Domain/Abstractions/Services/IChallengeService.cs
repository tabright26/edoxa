﻿// Filename: IChallengeService.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Arena.Challenges.Domain.Abstractions.Services
{
    public interface IChallengeService
    {
        Task<IEnumerable<IChallenge>> FakeChallengesAsync(
            int count,
            int seed,
            Game game = null,
            ChallengeState state = null,
            CurrencyType entryFeeCurrency = null,
            CancellationToken cancellationToken = default
        );

        Task<Participant> RegisterParticipantAsync(
            ChallengeId challengeId,
            UserId userId,
            Func<Game, GameAccountId> funcUserGameReference,
            CancellationToken cancellationToken = default
        );

        Task CloseAsync(CancellationToken cancellationToken = default);

        Task SynchronizeAsync(Game game, CancellationToken cancellationToken = default);
    }
}
