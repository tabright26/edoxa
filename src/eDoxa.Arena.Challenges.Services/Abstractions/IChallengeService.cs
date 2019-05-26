// Filename: IChallengeService.cs
// Date Created: 2019-05-25
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Domain;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Entities;
using eDoxa.Seedwork.Domain.Enumerations;
using eDoxa.Seedwork.Domain.Validations;

namespace eDoxa.Arena.Challenges.Services.Abstractions
{
    public interface IChallengeService
    {
        Task<Either<ValidationError, Challenge>> CreateChallengeAsync(
            string name,
            Game game,
            int duration,
            int bestOf,
            int payoutEntries,
            decimal entryFee,
            Currency currency,
            bool isFake = false,
            CancellationToken cancellationToken = default
        );

        Task<Either<ValidationError, Participant>> RegisterParticipantAsync(
            ChallengeId challengeId,
            UserId userId,
            Func<Game, ExternalAccount> funcExternalAccount,
            CancellationToken cancellationToken = default
        );

        Task CompleteAsync(CancellationToken cancellationToken = default);

        Task SynchronizeAsync(Game game, CancellationToken cancellationToken = default);
    }
}
