// Filename: NotificationsApiModule.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Notifications.Api.Application.Services;
using eDoxa.Notifications.Api.Infrastructure.Data;
using eDoxa.Notifications.Domain.Repositories;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Notifications.Infrastructure.Repositories;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

namespace eDoxa.Notifications.Api.Infrastructure
{
    internal sealed class NotificationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Cleaner
            builder.RegisterType<NotificationsDbContextCleaner>().As<IDbContextCleaner>().InstancePerLifetimeScope();

            // Seeder
            builder.RegisterType<NotificationsDbContextSeeder>().As<IDbContextSeeder>().InstancePerLifetimeScope();

            // Repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().As<IEmailService>().SingleInstance();
            builder.RegisterType<RedirectService>().As<IRedirectService>().SingleInstance();
        }
    }
}
