// Filename: MatchDto.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Games.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeMatch
    {
        public ChallengeMatch(string gameUuid, IDateTimeProvider gameCreatedAt, TimeSpan gameDuration, IDictionary<string, double> stats)
        {
            GameUuid = gameUuid;
            GameCreatedAt = gameCreatedAt.DateTime;
            GameDuration = gameDuration;
            Stats = stats;
        }

        public string GameUuid { get; }

        public DateTime GameCreatedAt { get; }

        public TimeSpan GameDuration { get; }

        public IDictionary<string, double> Stats { get; }
    }
}
