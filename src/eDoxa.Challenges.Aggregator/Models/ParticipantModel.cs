// Filename: ParticipantModel.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Challenges.Aggregator.Models
{
    public class ParticipantModel
    {
        public Guid Id { get; set; }

        public decimal? Score { get; set; }

        public Guid ChallengeId { get; set; }

        public UserModel User { get; set; }

        public MatchModel[] Matches { get; set; }
    }
}
