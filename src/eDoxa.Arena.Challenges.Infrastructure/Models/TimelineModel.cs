using System;

namespace eDoxa.Arena.Challenges.Infrastructure.Models
{
    public class TimelineModel
    {
        public long Duration { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? ClosedAt { get; set; }
    }
}
