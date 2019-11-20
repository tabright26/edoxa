// Filename: HangfireOptions.cs
// Date Created: 2019-11-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

namespace eDoxa.Challenges.Web.Jobs.Infrastructure
{
    public sealed class HangfireOptions
    {
        public IDictionary<string, string> RecurringJobs { get; set; }
    }
}
