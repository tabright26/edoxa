﻿// Filename: ChallengeTimelineModel.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Challenges.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 2.2.
    /// </remarks>
    public class ChallengeTimelineModel
    {
        public DateTime CreatedAt { get; set; }

        public long Duration { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? ClosedAt { get; set; }
    }
}