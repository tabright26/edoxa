// Filename: PaymentWebApplicationFactory.cs
// Date Created: 2019-07-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Payment.Api;
using eDoxa.Seedwork.Testing;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eDoxa.FunctionalTests.Services.Payment
{
    public sealed class PaymentWebApplicationFactory : CustomWebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost( IWebHostBuilder builder)
        {
            builder.UseContentRoot(
                Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(PaymentWebApplicationFactory)).Location), "Services/Payment")
            );

            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());
        }
    }
}
