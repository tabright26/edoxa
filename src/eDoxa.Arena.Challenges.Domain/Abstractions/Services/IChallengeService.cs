// Filename: IChallengeService.cs
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Abstractions.Services
{
    public interface IChallengeService
    {
        Task<IEnumerable<Challenge>> FakeChallengesAsync(
            int count,
            int? seed = null,
            Game game = null,
            ChallengeState state = null,
            CurrencyType entryFeeCurrency = null,
            CancellationToken cancellationToken = default
        );

        Task<Participant> RegisterParticipantAsync(
            ChallengeId challengeId,
            UserId userId,
            Func<Game, ExternalAccount> funcExternalAccount,
            CancellationToken cancellationToken = default
        );

        Task CloseAsync(CancellationToken cancellationToken = default);

        Task SynchronizeAsync(Game game, CancellationToken cancellationToken = default);
    }
}
