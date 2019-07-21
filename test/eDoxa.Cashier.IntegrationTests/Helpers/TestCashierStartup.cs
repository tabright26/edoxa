// Filename: TestCashierStartup.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Api;
using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Cashier.IntegrationTests.Helpers
{
    internal class TestCashierStartup : Startup
    {
        public TestCashierStartup(IConfiguration configuration, IHostingEnvironment environment) : base(configuration, environment)
        {
        }

        protected override IServiceProvider BuildModule(IServiceCollection services)
        {
            return services.Build<TestCashierModule>();
        }
    }
}
