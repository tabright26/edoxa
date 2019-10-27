// Filename: GamesDbContext.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Games.Infrastructure
{
    public sealed class GamesDbContext : DbContext
    {
        public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
        {
        }
    }
}
