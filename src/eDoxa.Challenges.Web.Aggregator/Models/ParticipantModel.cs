// Filename: ParticipantModel.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

namespace eDoxa.Challenges.Web.Aggregator.Models
{
    public class ParticipantModel
    {
        public string Id { get; set; }

        public decimal? Score { get; set; }

        public string ChallengeId { get; set; }

        public UserModel User { get; set; }

        public MatchModel[] Matches { get; set; }
    }
}
