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
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using FluentValidation.Results;

namespace eDoxa.Arena.Challenges.Services.Abstractions
{
    public interface IChallengeService
    {
        Task<Either<ValidationResult, Challenge>> CreateChallengeAsync(
            string name,
            Game game,
            int duration,
            int bestOf,
            int payoutEntries,
            EntryFee entryFee,
            ChallengeState testModeState = null,
            CancellationToken cancellationToken = default
        );

        Task<Either<ValidationResult, Participant>> RegisterParticipantAsync(
            ChallengeId challengeId,
            UserId userId,
            Func<Game, ExternalAccount> funcExternalAccount,
            CancellationToken cancellationToken = default
        );

        Task CompleteAsync(ChallengeId challengeId, CancellationToken cancellationToken = default);

        Task SynchronizeAsync(ChallengeId challengeId, CancellationToken cancellationToken = default);
    }
}
