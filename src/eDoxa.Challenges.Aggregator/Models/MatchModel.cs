// Filename: MatchModel.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Challenges.Aggregator.Models
{
    public class MatchModel
    {
        public Guid Id { get; set; }

        public decimal Score { get; set; }

        public Guid ParticipantId { get; set; }

        public StatModel[] Stats { get; set; }
    }
}
