// Filename: ChallengeService.cs
// Date Created: 2019-11-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Worker.Application.RecurringJobs
{
    public sealed class ChallengeRecurringJob
    {
        public Task SynchronizeChallengeAsync(Game game)
        {
            return Task.CompletedTask;
        }
    }
}
