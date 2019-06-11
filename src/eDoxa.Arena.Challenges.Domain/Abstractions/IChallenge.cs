// Filename: IChallenge.cs
// Date Created: 2019-06-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Abstractions.Factories;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Abstractions
{
    public interface IChallenge
    {
        ChallengeId Id { get; }

        Game Game { get; }

        ChallengeName Name { get; }

        ChallengeSetup Setup { get; }

        ChallengeTimeline Timeline { get; }

        DateTime CreatedAt { get; }

        DateTime? LastSync { get; }

        IReadOnlyCollection<ChallengeStat> Stats { get; }

        IReadOnlyCollection<Participant> Participants { get; }

        IReadOnlyCollection<Bucket> Buckets { get; }

        void EnableTestMode(TestMode testMode, ChallengeTimeline timeline);

        Task SynchronizeAsync(IMatchReferencesFactory matchReferencesFactory, IMatchStatsFactory matchStatsFactory);
    }
}
