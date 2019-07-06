// Filename: CashierStartup.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Api;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eDoxa.FunctionalTests.Services.Cashier.Helpers
{
    public class TestCashierStartup : Startup
    {
        public TestCashierStartup(IConfiguration configuration, IHostingEnvironment environment) : base(configuration, environment)
        {
        }
    }
}
