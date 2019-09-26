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

        public DbSet<Clan> Clans => this.Set<Clan>();

        public DbSet<Member> Members => this.Set<Member>();

        public DbSet<Candidature> Candidatures => this.Set<Candidature>();

        public DbSet<Invitation> Invitations => this.Set<Invitation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clan>(
                builder =>
                {
                    builder.ToTable("Clan");

                    builder.Property(clan => clan.Id)
                        .HasConversion(clanId => clanId.ToGuid(), value => ClanId.FromGuid(value))
                        .IsRequired();

                    builder.Property(clan => clan.Name).IsRequired();

                    builder.Property(clan => clan.Summary).IsRequired(false);

                    builder.Property(clan => clan.OwnerId).HasConversion(invitationId => invitationId.ToGuid(), value => UserId.FromGuid(value)).IsRequired();

                    builder.HasMany(clan => clan.Members).WithOne().HasForeignKey(member => member.ClanId);

                    builder.HasKey(clan => clan.Id);
                });

                    modelBuilder.Entity<Clan>(
                builder =>
                {
                    builder.ToTable("Clan");

                    builder.Property(clan => clan.Id)
                        .HasConversion(clanId => clanId.ToGuid(), value => ClanId.FromGuid(value))
                        .IsRequired();

                    builder.Property(clan => clan.Name).IsRequired();

                    builder.Property(clan => clan.Summary).IsRequired(false);

                    builder.Property(clan => clan.OwnerId).HasConversion(invitationId => invitationId.ToGuid(), value => UserId.FromGuid(value)).IsRequired();

                    builder.HasMany(clan => clan.Members).WithOne().HasForeignKey(member => member.ClanId);

                    builder.HasKey(clan => clan.Id);
                });

            modelBuilder.Entity<Member>(
                builder =>
                {
                    builder.ToTable("Member");

                    builder.Property(member => member.Id)
                        .HasConversion(memberId => memberId.ToGuid(), value => MemberId.FromGuid(value))
                        .IsRequired();

                    builder.Property(member => member.UserId).HasConversion(userId => userId.ToGuid(), value => UserId.FromGuid(value)).IsRequired();
                    builder.Property(member => member.ClanId).HasConversion(clanId => clanId.ToGuid(), value => ClanId.FromGuid(value)).IsRequired();

                    builder.HasKey(member => member.Id);
                });

            modelBuilder.Entity<Candidature>(
                builder =>
                {
                    builder.ToTable("Candidature");

                    builder.Property(candidature => candidature.Id)
                        .HasConversion(candidatureId => candidatureId.ToGuid(), value => CandidatureId.FromGuid(value))
                        .IsRequired();

                    builder.Property(candidature => candidature.UserId).HasConversion(userId => userId.ToGuid(), value => UserId.FromGuid(value)).IsRequired();
                    builder.Property(candidature => candidature.ClanId).HasConversion(clanId => clanId.ToGuid(), value => ClanId.FromGuid(value)).IsRequired();
                    builder.HasKey(candidature => candidature.Id);
                });

            modelBuilder.Entity<Invitation>(
                builder =>
                {
                    builder.ToTable("Invitation");

                    builder.Property(invitation => invitation.Id)
                        .HasConversion(invitationId => invitationId.ToGuid(), value => InvitationId.FromGuid(value))
                        .IsRequired();

                    builder.Property(invitation => invitation.UserId).HasConversion(userId => userId.ToGuid(), value => UserId.FromGuid(value)).IsRequired();
                    builder.Property(invitation => invitation.ClanId).HasConversion(clanId => clanId.ToGuid(), value => ClanId.FromGuid(value)).IsRequired();
                    builder.HasKey(invitation => invitation.Id);
                });
        }
    }
}
