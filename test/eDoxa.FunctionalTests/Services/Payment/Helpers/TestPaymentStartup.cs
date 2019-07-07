// Filename: PaymentStartup.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Payment.Api;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eDoxa.FunctionalTests.Services.Payment.Helpers
{
    public class TestPaymentStartup : Startup
    {
        public TestPaymentStartup(IConfiguration configuration, IHostingEnvironment environment) : base(configuration, environment)
        {
        }
    }
}
