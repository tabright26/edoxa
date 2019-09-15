// Filename: ClansDbContext.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Organizations.Clans.Infrastructure
{
    public class ClansDbContext : DbContext
    {
        public ClansDbContext(DbContextOptions<ClansDbContext> options) : base(options)
        {
        }

        public DbSet<ClanModel> Clans => this.Set<ClanModel>();

        public DbSet<MemberModel> Members => this.Set<MemberModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClanModel>().HasMany(clan => clan.Members).WithOne(member => member.Clan);
        }
    }
}
