// Filename: ChallengeModel.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Grpc.Protos.Challenges.Enums;
using eDoxa.Grpc.Protos.Shared.Enums;

namespace eDoxa.Challenges.Web.Aggregator.Models
{
    public class ChallengeModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Game Game { get; set; }

        public ChallengeState State { get; set; }

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
