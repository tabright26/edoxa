// Filename: ChallengesWorkerHostFactory.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Challenges.Worker;
using eDoxa.Seedwork.TestHelper;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.FunctionalTests.Workers.Challenges
{
    public sealed class ChallengesWorkerHostFactory : WebHostFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.UseContentRoot(
                Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(ChallengesWorkerHostFactory)).Location), "Workers/Challenges"));

            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());

            base.ConfigureWebHost(builder);
        }

        protected override void ConfigureTestServices(IServiceCollection services)
        {
        }

        protected override void ContainerTestBuilder(ContainerBuilder builder)
        {
        }
    }
}
