// Filename: ChallengeModel.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

namespace eDoxa.Challenges.Aggregator.Models
{
    public class ChallengeModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Game { get; set; }

        public string State { get; set; }

        public int BestOf { get; set; }

        public int Entries { get; set; }

        public int PayoutEntries { get; set; }

        public long? SynchronizedAt { get; set; }

        public IDictionary<string, float> Scoring { get; set; }

        public EntryFeeModel EntryFee { get; set; }

        public PayoutModel Payout { get; set; }

        public TimelineModel Timeline { get; set; }

        public ParticipantModel[] Participants { get; set; }
    }
}
