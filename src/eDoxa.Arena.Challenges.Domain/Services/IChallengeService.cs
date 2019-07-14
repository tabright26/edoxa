﻿// Filename: IChallengeService.cs
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.Services
{
    public interface IChallengeService
    {
        Task RegisterParticipantAsync(ChallengeId challengeId, UserId userId, IDateTimeProvider registeredAt, CancellationToken cancellationToken = default);

        Task SynchronizeAsync(ChallengeGame game, TimeSpan interval, IDateTimeProvider synchronizedAt, CancellationToken cancellationToken = default);

        Task CloseAsync(IDateTimeProvider closedAt, CancellationToken cancellationToken = default);
    }
}
