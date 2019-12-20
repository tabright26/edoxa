﻿// Filename: TestApiFixture.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api;
using eDoxa.Payment.Infrastructure;
using eDoxa.Seedwork.TestHelper;
using eDoxa.Seedwork.TestHelper.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace eDoxa.Payment.TestHelper.Fixtures
{
    public sealed class TestHostFixture : WebHostFactory<Startup>
    {
        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            server.MigrateDbContext<PaymentDbContext>();

            return server;
        }
    }
}
