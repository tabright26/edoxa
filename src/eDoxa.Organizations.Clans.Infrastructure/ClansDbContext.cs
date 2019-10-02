// Filename: ClansDbContext.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.Extensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

namespace eDoxa.Organizations.Clans.Infrastructure
{
    public class ClansDbContext : DbContext, IUnitOfWork
    {
        public ClansDbContext(DbContextOptions<ClansDbContext> options, IMediator mediator) : this(options)
        {
            Mediator = mediator;
        }

        public ClansDbContext(DbContextOptions<ClansDbContext> options) : base(options)
        {
            Mediator = new Mock<IMediator>().Object;
        }

        private IMediator Mediator { get; }

        public DbSet<Clan> Clans => this.Set<Clan>();

        public DbSet<Member> Members => this.Set<Member>();

        public DbSet<Candidature> Candidatures => this.Set<Candidature>();

        public DbSet<Invitation> Invitations => this.Set<Invitation>();

        public async Task CommitAsync(bool dispatchDomainEvents = true, CancellationToken cancellationToken = default)
        {
            await this.SaveChangesAsync(cancellationToken);

            if (dispatchDomainEvents)
            {
                var entities = ChangeTracker.Entries<IEntity>().Select(entry => entry.Entity).Where(entity => entity.DomainEvents.Any()).ToList();

                var domainEvents = entities.SelectMany(entity => entity.DomainEvents).ToList();

                foreach (var entity in entities)
                {
                    entity.ClearDomainEvents();
                }

                foreach (var domainEvent in domainEvents)
                {
                    await Mediator.PublishDomainEventAsync(domainEvent);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clan>(
                builder =>
                {
                    builder.ToTable("Clan");

                    builder.Property(clan => clan.Id).HasConversion(clanId => clanId.ToGuid(), value => ClanId.FromGuid(value)).IsRequired();

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

                    builder.Property(clan => clan.Id).HasConversion(clanId => clanId.ToGuid(), value => ClanId.FromGuid(value)).IsRequired();

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

                    builder.Property(member => member.Id).HasConversion(memberId => memberId.ToGuid(), value => MemberId.FromGuid(value)).IsRequired();

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
