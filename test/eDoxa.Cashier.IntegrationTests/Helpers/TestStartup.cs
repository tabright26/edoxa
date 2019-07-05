// Filename: TestStartup.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Api;
using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Cashier.IntegrationTests.Helpers
{
    internal class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IHostingEnvironment environment) : base(configuration, environment)
        {
        }

        protected override IServiceProvider BuildModule(IServiceCollection services)
        {
            return services.Build<TestModule>();
        }
    }
}
