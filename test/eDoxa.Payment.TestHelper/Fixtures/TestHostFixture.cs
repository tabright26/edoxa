// Filename: TestHostFixture.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using Autofac;

using eDoxa.Payment.Api;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.Infrastructure;
using eDoxa.Seedwork.TestHelper;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

using Moq;

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
