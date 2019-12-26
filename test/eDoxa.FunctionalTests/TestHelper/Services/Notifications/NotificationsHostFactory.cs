// Filename: NotificationsHostFactory.cs
// Date Created: 2019-12-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Notifications.Api;
using eDoxa.Notifications.Infrastructure;
using eDoxa.Seedwork.TestHelper;
using eDoxa.Seedwork.TestHelper.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace eDoxa.FunctionalTests.TestHelper.Services.Notifications
{
    public sealed class NotificationsHostFactory : WebHostFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseCustomContentRoot("TestHelper/Services/Clans");
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            server.EnsureCreatedDbContext<NotificationsDbContext>();

            return server;
        }
    }
}
