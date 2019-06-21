// Filename: TimelineModel.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Arena.Challenges.Infrastructure.Models
{
    public class ChallengeTimelineModel
    {
        public long Duration { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? ClosedAt { get; set; }
    }
}
