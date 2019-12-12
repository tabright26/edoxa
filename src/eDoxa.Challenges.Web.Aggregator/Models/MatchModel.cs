﻿// Filename: MatchModel.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

namespace eDoxa.Challenges.Web.Aggregator.Models
{
    public class MatchModel
    {
        public string Id { get; set; }

        public decimal Score { get; set; }

        public string ParticipantId { get; set; }

        public StatModel[] Stats { get; set; }
    }
}
