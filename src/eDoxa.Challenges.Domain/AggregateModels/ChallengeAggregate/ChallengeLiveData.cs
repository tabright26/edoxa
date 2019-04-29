// Filename: ChallengeLiveData.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories;
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeLiveData : ValueObject
    {
        private readonly IReadOnlyCollection<Participant> _participants;
        private readonly ChallengeSetup _setup;

        public ChallengeLiveData(ChallengeSetup setup, IReadOnlyCollection<Participant> participants)
        {
            _setup = setup;
            _participants = participants;
        }

        public Entries Entries => new Entries(_participants.Count, false);

        public PayoutEntries PayoutEntries => new PayoutEntries(Entries, _setup.PayoutRatio);

        public PrizePool PrizePool => new PrizePool(Entries, _setup.EntryFee, _setup.ServiceChargeRatio);

        public IPayout Payout => ChallengePayoutFactory.Instance.CreatePayout(_setup.Type, PayoutEntries, PrizePool, _setup.EntryFee).Payout;
    }
}