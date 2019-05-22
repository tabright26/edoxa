// Filename: ChallengeFactory.cs
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
        private static readonly Lazy<ChallengeFactory> Lazy = new Lazy<ChallengeFactory>(() => new ChallengeFactory());

        public static ChallengeFactory Instance => Lazy.Value;

        public Challenge Create(Game game, ChallengeType type)
        {
            var name = new ChallengeName($"Fake challenge ({type})");
            var duration = new ChallengeDuration();
            var strategy = ScoringFactory.Instance.CreateScoringStrategy(game);

            if (type.Equals(ChallengeType.Type1))
            {
                var setup = new ChallengeSetup(
                    new BestOf(1),
                    new Entries(10),
                    new EntryFee(2.5M),
                    new PayoutRatio(0.3F),
                    new ServiceChargeRatio(0.2F)
                );

                var payout = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(10M), 1),
                        new Bucket(new Prize(7.5M), 1),
                        new Bucket(new Prize(2.5M), 1)
                    }
                );

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
                var setup = new ChallengeSetup(
                    new BestOf(3),
                    new Entries(30),
                    new EntryFee(2.5M),
                    new PayoutRatio(0.4F),
                    new ServiceChargeRatio(0.2F)
                );

                var payout = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(14M), 1),
                        new Bucket(new Prize(8M), 2),
                        new Bucket(new Prize(5M), 3),
                        new Bucket(new Prize(2.5M), 6)
                    }
                );

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
                var setup = new ChallengeSetup(
                    new BestOf(3),
                    new Entries(30),
                    new EntryFee(5M),
                    new PayoutRatio(0.5F),
                    new ServiceChargeRatio(0.2F)
                );

                var payout = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(20M), 1),
                        new Bucket(new Prize(15M), 1),
                        new Bucket(new Prize(10M), 2),
                        new Bucket(new Prize(7M), 4),
                        new Bucket(new Prize(5M), 7)
                    }
                );

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
                var setup = new ChallengeSetup(
                    new BestOf(3),
                    new Entries(30),
                    new EntryFee(10M),
                    new PayoutRatio(0.5F),
                    new ServiceChargeRatio(0.2F)
                );

                var payout = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(40M), 1),
                        new Bucket(new Prize(30M), 1),
                        new Bucket(new Prize(20M), 2),
                        new Bucket(new Prize(14M), 4),
                        new Bucket(new Prize(10M), 7)
                    }
                );

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
                var setup = new ChallengeSetup(
                    new BestOf(3),
                    new Entries(50),
                    new EntryFee(5M),
                    new PayoutRatio(0.5F),
                    new ServiceChargeRatio(0.2F)
                );

                var payout = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(25M), 1),
                        new Bucket(new Prize(20M), 1),
                        new Bucket(new Prize(12.5M), 2),
                        new Bucket(new Prize(10M), 3),
                        new Bucket(new Prize(7M), 5),
                        new Bucket(new Prize(5M), 13)
                    }
                );

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
                var setup = new ChallengeSetup(
                    new BestOf(3),
                    new Entries(50),
                    new EntryFee(10M),
                    new PayoutRatio(0.5F),
                    new ServiceChargeRatio(0.2F)
                );

                var payout = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(50M), 1),
                        new Bucket(new Prize(40M), 1),
                        new Bucket(new Prize(25M), 2),
                        new Bucket(new Prize(20M), 3),
                        new Bucket(new Prize(14M), 5),
                        new Bucket(new Prize(10M), 13)
                    }
                );

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
