// Filename: ChallengeLiveData.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeLiveData
    {
        private static readonly ChallengePayoutFactory ChallengePayoutFactory = ChallengePayoutFactory.Instance;

        private readonly IReadOnlyCollection<Participant> _participants;
        private readonly ChallengeSettings _settings;

        public ChallengeLiveData(ChallengeSettings settings, IReadOnlyCollection<Participant> participants)
        {
            _settings = settings;
            _participants = participants;
        }

        public Entries Entries => new Entries(_participants.Count, false);

        public PayoutEntries PayoutEntries => new PayoutEntries(Entries, _settings.PayoutRatio);

        public PrizePool PrizePool => new PrizePool(Entries, _settings.EntryFee, _settings.ServiceChargeRatio);

        public IChallengePayout Payout => ChallengePayoutFactory.CreatePayout(_settings.Type, PayoutEntries, PrizePool, _settings.EntryFee).Payout;
    }
}