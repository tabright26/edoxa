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
        public ChallengeMatch(string gameUuid, IDateTimeProvider provider, IDictionary<string, double> stats)
        {
            GameUuid = gameUuid;
            Timestamp = provider.DateTime;
            Stats = stats;
        }

        public string GameUuid { get; }

        public DateTime Timestamp { get; }

        public IDictionary<string, double> Stats { get; }
    }
}
