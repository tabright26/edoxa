// Filename: ChallengeFactory.cs
// Date Created: 2019-05-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Challenges.Services.Factories
{
    public sealed class ChallengeFactory
    {
        private static readonly Lazy<ChallengeFactory> Lazy = new Lazy<ChallengeFactory>(() => new ChallengeFactory());

        public static ChallengeFactory Instance => Lazy.Value;

        public Challenge Create(Game game, ChallengeType type)
        {
            var name = new ChallengeName($"Fake challenge ({type})");
            var duration = new ChallengeDuration();
            var strategy = ScoringFactory.Instance.CreateScoringStrategy(game);

            if (type.Equals(ChallengeType.Type1))
            {
                var setup = new ChallengeSetup(BestOf.One, PayoutEntries.Five, MoneyEntryFee.TwoAndHalf);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    strategy
                );
            }

            if (type.Equals(ChallengeType.Type2))
            {
                var setup = new ChallengeSetup(BestOf.Three, PayoutEntries.Fifteen, MoneyEntryFee.TwoAndHalf);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    strategy
                );
            }

            if (type.Equals(ChallengeType.Type3))
            {
                var setup = new ChallengeSetup(BestOf.Three, PayoutEntries.Fifteen, MoneyEntryFee.Five);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    strategy
                );
            }

            if (type.Equals(ChallengeType.Type4))
            {
                var setup = new ChallengeSetup(BestOf.Three, PayoutEntries.Fifteen, MoneyEntryFee.Ten);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    strategy
                );
            }

            if (type.Equals(ChallengeType.Type5))
            {
                var setup = new ChallengeSetup(BestOf.Three, PayoutEntries.TwentyFive, MoneyEntryFee.Five);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    strategy
                );
            }

            if (type.Equals(ChallengeType.Type6))
            {
                var setup = new ChallengeSetup(BestOf.Three, PayoutEntries.TwentyFive, MoneyEntryFee.Ten);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    strategy
                );
            }

            throw new NotImplementedException();
        }
    }
}
