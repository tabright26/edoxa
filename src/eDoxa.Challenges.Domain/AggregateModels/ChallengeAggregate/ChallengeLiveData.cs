// Filename: ChallengeLiveData.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeLiveData
    {
        private static readonly ChallengeHelper Helper = new ChallengeHelper();

        private readonly Challenge _challenge;

        public ChallengeLiveData(Challenge challenge)
        {
            _challenge = challenge;
        }

        public int Entries
        {
            get
            {
                return _challenge.Participants.Count;
            }
        }

        public int PayoutEntries
        {
            get
            {
                return Helper.PayoutEntries(Entries, _challenge.Settings.PayoutRatio);
            }
        }

        public decimal PrizePool
        {
            get
            {
                return Helper.PrizePool(Entries, _challenge.Settings.EntryFee, _challenge.Settings.ServiceChargeRatio);
            }
        }

        public IChallengePrizeBreakdown PrizeBreakdown
        {
            get
            {
                var factory = ChallengePrizeBreakdownFactory.Instance;

                var strategy = factory.Create(_challenge.Settings.Type, PayoutEntries, PrizePool);

                return strategy.PrizeBreakdown;
            }
        }
    }
}