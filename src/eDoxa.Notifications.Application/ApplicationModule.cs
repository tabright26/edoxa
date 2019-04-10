// Filename: NotificationsModule.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Autofac;
using eDoxa.Notifications.Application.Queries;
using eDoxa.Notifications.Domain.Repositories;
using eDoxa.Notifications.DTO.Queries;
using eDoxa.Notifications.Infrastructure;
using eDoxa.Notifications.Infrastructure.Repositories;

namespace eDoxa.Notifications.Application
{
    public sealed class ApplicationModule : CustomServiceModule<ApplicationModule, NotificationsDbContext>
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationRepository>().As<INotificationRepository>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<NotificationQueries>().As<INotificationQueries>().InstancePerLifetimeScope();
        }
    }
}