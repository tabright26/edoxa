// Filename: ChallengeGame.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeGame
    {
        private readonly string _displayName;
        private readonly long _value;

        private ChallengeGame(long value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        public static ChallengeGame None { get; } = new ChallengeGame(0, nameof(None));

        public static ChallengeGame LeagueOfLegends { get; } = new ChallengeGame(1 << 0, nameof(LeagueOfLegends));

        public static ChallengeGame CSGO { get; } = new ChallengeGame(1 << 1, nameof(CSGO));

        public static ChallengeGame Fortnite { get; } = new ChallengeGame(1 << 2, nameof(Fortnite));

        public static ChallengeGame All { get; } = new ChallengeGame(~None, nameof(All));

        public static implicit operator long(ChallengeGame game)
        {
            return game._value;
        }

        public override string ToString()
        {
            return _displayName;
        }
    }
}