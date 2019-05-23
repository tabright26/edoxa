﻿// Filename: ChallengeFactory.cs
// Date Created: 2019-05-22
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
        private static readonly PayoutFactory PayoutFactory = PayoutFactory.Instance;
        private static readonly Lazy<ChallengeFactory> Lazy = new Lazy<ChallengeFactory>(() => new ChallengeFactory());

        public static ChallengeFactory Instance => Lazy.Value;

        public Challenge Create(Game game, ChallengeType type)
        {
            var name = new ChallengeName($"Fake challenge ({type})");
            var duration = new ChallengeDuration();
            var strategy = ScoringFactory.Instance.CreateScoringStrategy(game);

            if (type.Equals(ChallengeType.Type1))
            {
                var setup = new ChallengeSetup(new BestOf(1), new Entries(10), new EntryFee(2.5M));

                var payout = PayoutFactory.Create(PayoutEntries.Five, MoneyPayoutFactor.TwoAndHalf);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    payout,
                    strategy
                );
            }

            if (type.Equals(ChallengeType.Type2))
            {
                var setup = new ChallengeSetup(new BestOf(3), new Entries(30), new EntryFee(2.5M));

                var payout = PayoutFactory.Create(PayoutEntries.Fifteen, MoneyPayoutFactor.TwoAndHalf);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    payout,
                    strategy
                );
            }

            if (type.Equals(ChallengeType.Type3))
            {
                var setup = new ChallengeSetup(new BestOf(3), new Entries(30), new EntryFee(5M));

                var payout = PayoutFactory.Create(PayoutEntries.Fifteen, MoneyPayoutFactor.Five);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    payout,
                    strategy
                );
            }

            if (type.Equals(ChallengeType.Type4))
            {
                var setup = new ChallengeSetup(new BestOf(3), new Entries(30), new EntryFee(10M));

                var payout = PayoutFactory.Create(PayoutEntries.Fifteen, MoneyPayoutFactor.Ten);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    payout,
                    strategy
                );
            }

            if (type.Equals(ChallengeType.Type5))
            {
                var setup = new ChallengeSetup(new BestOf(3), new Entries(50), new EntryFee(5M));

                var payout = PayoutFactory.Create(PayoutEntries.TwentyFive, MoneyPayoutFactor.Five);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    payout,
                    strategy
                );
            }

            if (type.Equals(ChallengeType.Type6))
            {
                var setup = new ChallengeSetup(new BestOf(3), new Entries(50), new EntryFee(10M));

                var payout = PayoutFactory.Create(PayoutEntries.TwentyFive, MoneyPayoutFactor.Ten);

                return new Challenge(
                    game,
                    name,
                    setup,
                    duration,
                    payout,
                    strategy
                );
            }

            throw new NotImplementedException();
        }
    }
}
