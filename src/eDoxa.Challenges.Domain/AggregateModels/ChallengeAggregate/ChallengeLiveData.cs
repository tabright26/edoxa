// Filename: ChallengeLiveData.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories;
using eDoxa.Challenges.Domain.ValueObjects;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeLiveData
    {
        private readonly Challenge _challenge;

        public ChallengeLiveData(Challenge challenge)
        {
            _challenge = challenge;
        }

        public Entries Entries => new Entries(_challenge.Participants.Count, false);

        public PayoutEntries PayoutEntries => new PayoutEntries(Entries, _challenge.Settings.PayoutRatio);

        public PrizePool PrizePool => new PrizePool(Entries, _challenge.Settings.EntryFee, _challenge.Settings.ServiceChargeRatio);

        public IChallengePrizeBreakdown PrizeBreakdown
        {
            get
            {
                var factory = ChallengePrizeBreakdownFactory.Instance;

                var strategy = factory.Create(_challenge.Settings.Type, PayoutEntries.ToInt32(), PrizePool.ToDecimal());

                return strategy.PrizeBreakdown;
            }
        }
    }
}