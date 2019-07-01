using System.IO;
using System.Reflection;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Seedwork.Security.Hosting;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers
{
    public static class TestServerHelper
    {
        public static TestServer CreateTestServer<TDbContext>()
        where TDbContext : DbContext
        {
            var hostBuilder = new WebHostBuilder();

            hostBuilder.UseEnvironment(EnvironmentNames.Testing);

            hostBuilder.UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location));

            hostBuilder.ConfigureAppConfiguration(configuration => configuration.AddJsonFile("appsettings.Testing.json", false).AddEnvironmentVariables());

            hostBuilder.UseStartup<Startup>();

            hostBuilder.ConfigureServices(
                services =>
                {
                }
            );

            var testServer = new TestServer(hostBuilder);

            testServer.EnsureCreated<TDbContext>();

            return testServer;
        }
    }
}
