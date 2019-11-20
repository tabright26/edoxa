﻿// Filename: NotificationsApiModule.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Notifications.Api.Areas.Email.Services;
using eDoxa.Notifications.Domain.Services;

namespace eDoxa.Notifications.Api.Infrastructure
{
    internal sealed class NotificationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Services
            builder.RegisterType<EmailService>().As<IEmailService>().SingleInstance();
        }
    }
}