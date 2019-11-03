// Filename: ChallengeModel.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

namespace eDoxa.Challenges.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 2.2.
    /// </remarks>
    public class ChallengeModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Game { get; set; }

        public int State { get; set; }

        public int BestOf { get; set; }

        public int Entries { get; set; }

        public DateTime? SynchronizedAt { get; set; }

        public ChallengeTimelineModel Timeline { get; set; }

        public ICollection<ScoringItemModel> ScoringItems { get; set; }

        public ICollection<ParticipantModel> Participants { get; set; }
    }
}
