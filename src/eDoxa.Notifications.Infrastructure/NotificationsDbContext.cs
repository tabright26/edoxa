// Filename: NotificationsDbContext.cs
// Date Created: 2019-04-07
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.Domain.Factories;
using eDoxa.Notifications.Infrastructure.Configurations;
using eDoxa.Seedwork.Infrastructure;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Notifications.Infrastructure
{
    public sealed partial class NotificationsDbContext
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        public async Task SeedAsync(ILogger logger)
        {
            if (!Users.Any())
            {
                Users.AddRange(
                    _userAggregateFactory.CreateAdmin(true),
                    _userAggregateFactory.CreateFrancis(true),
                    _userAggregateFactory.CreateRoy(true),
                    _userAggregateFactory.CreateRyan(true)
                );

                await this.SaveChangesAsync();

                logger.LogInformation("The users being populated.");
            }
            else
            {
                logger.LogInformation("The users already populated.");
            }
        }
    }

    public sealed partial class NotificationsDbContext : CustomDbContext
    {
        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        internal NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users
        {
            get
            {
                return this.Set<User>();
            }
        }

        public DbSet<Notification> Notifications
        {
            get
            {
                return this.Set<Notification>();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(nameof(eDoxa).ToLower());

            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new NotificationConfiguration());
        }
    }
}