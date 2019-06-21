// Filename: ParticipantModel.cs
// Date Created: 2019-06-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Infrastructure;

namespace eDoxa.Arena.Challenges.Infrastructure.Models
{
    public class ParticipantModel : PersistentObject
    {
        public DateTime Timestamp { get; set; }

        public DateTime? LastSync { get; set; }

        public string GameAccountId { get; set; }

        public Guid UserId { get; set; }

        public Guid ChallengeId { get; set; }

        public ChallengeModel Challenge { get; set; }

        public ICollection<MatchModel> Matches { get; set; }
    }
}
