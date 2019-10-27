// Filename: GamesDbContext.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels.GameCredentialAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Games.Infrastructure
{
    public sealed class GamesDbContext : DbContext, IUnitOfWork
    {
        public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
        {
        }

        public DbSet<GameCredential> GameCredentials => this.Set<GameCredential>();

        public async Task CommitAsync(bool dispatchDomainEvents = true, CancellationToken cancellationToken = default)
        {
            await this.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameCredential>(
                builder =>
                {
                    builder.Property(gameCredential => gameCredential.UserId)
                        .HasConversion<Guid>(userId => userId, value => UserId.FromGuid(value))
                        .IsRequired();

                    builder.Property(gameCredential => gameCredential.Game).HasConversion(game => game.Value, value => Game.FromValue(value)).IsRequired();

                    builder.Property(gameCredential => gameCredential.PlayerId)
                        .HasConversion<Guid>(userId => userId, value => PlayerId.FromGuid(value))
                        .IsRequired();

                    builder.Property(gameCredential => gameCredential.Timestamp)
                        .HasConversion(dateTime => dateTime.Ticks, value => new DateTime(value))
                        .IsRequired();

                    builder.HasKey(
                        gameCredential => new
                        {
                            gameCredential.UserId,
                            gameCredential.Game,
                            gameCredential.PlayerId
                        });

                    builder.ToTable("GameCredential");
                });
        }
    }
}
