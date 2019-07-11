// Filename: ChallengeGame.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class ChallengeGame : Enumeration<ChallengeGame>
    {
        public static readonly ChallengeGame LeagueOfLegends = new ChallengeGame(1 << 0, nameof(LeagueOfLegends));

        public ChallengeGame()
        {
        }

        private ChallengeGame(int value, string name) : base(value, name)
        {
        }
    }
}
