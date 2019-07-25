// Filename: PaymentWebApplicationFactory.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Payment.Api;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security.AzureKeyVault.Extensions;
using eDoxa.Seedwork.Security.Hosting;
using eDoxa.Seedwork.Testing.Helpers;

using JetBrains.Annotations;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eDoxa.FunctionalTests.Services.Payment.Helpers
{
    public class PaymentWebApplicationFactory : CustomWebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentNames.Testing)
                .UseContentRoot(Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(PaymentWebApplicationFactory)).Location), "Services/Payment"))
                .ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());
        }

        [NotNull]
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder<Startup>(Array.Empty<string>()).UseAzureKeyVault().UseSerilog();
        }

        public override void WithContainerBuilder(Action<ContainerBuilder> builder)
        {
            Startup.ConfigureContainerBuilder += builder;
        }
    }
}
