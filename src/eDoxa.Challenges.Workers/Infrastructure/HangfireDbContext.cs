// Filename: HangfireDbContext.cs
// Date Created: 2019-11-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Workers.Infrastructure
{
    public sealed class HangfireDbContext : DbContext
    {
        public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options)
        {
        }
    }
}
