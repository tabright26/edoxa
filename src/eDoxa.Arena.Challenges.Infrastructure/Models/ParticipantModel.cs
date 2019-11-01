// Filename: ParticipantModel.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

namespace eDoxa.Arena.Challenges.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 2.2.
    /// </remarks>
    public class ParticipantModel
    {
        public Guid Id { get; set; }

        public DateTime RegisteredAt { get; set; }

        public string PlayerId { get; set; }

        public Guid UserId { get; set; }

        public ChallengeModel Challenge { get; set; }

        public ICollection<MatchModel> Matches { get; set; }
    }
}
