// Filename: TimelineModel.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Challenges.Aggregator.Models
{
    public class TimelineModel
    {
        public long CreatedAt { get; set; }

        public long? StartedAt { get; set; }

        public long? EndedAt { get; set; }

        public long? ClosedAt { get; set; }
    }
}
