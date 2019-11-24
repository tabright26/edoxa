// Filename: TestServerExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class TestServerExtensions
    {
        public static void EnsureCreatedDbContext<TDbContext>(this TestServer server)
        where TDbContext : DbContext
        {
            server.UsingScope(scope => scope?.GetRequiredService<TDbContext>().Database?.EnsureCreated());
        }

        public static void MigrateDbContext<TDbContext>(this TestServer server)
        where TDbContext : DbContext
        {
            server.UsingScope(scope => scope?.GetRequiredService<TDbContext>().Database?.Migrate());
        }

        public static void CleanupDbContext(this TestServer server)
        {
            server.UsingScope(
                scope =>
                {
                    var context = scope?.GetRequiredService<IDbContextCleaner>() ?? throw new InvalidOperationException();

                    context.CleanupAsync().Wait();
                }
            );
        }

        public static void UsingScope(this TestServer server, Action<IServiceScope> execute)
        {
            using var scope = server.Services.CreateScope();

            execute(scope);
        }

        public static async Task UsingScopeAsync(this TestServer server, Func<IServiceScope, Task> executeAsync)
        {
            using var scope = server.Services.CreateScope();

            await executeAsync(scope);
        }

        public static async Task<TResult> UsingScopeAsync<TResult>(this TestServer server, Func<IServiceScope, Task<TResult>> executeAsync)
        {
            using var scope = server.Services.CreateScope();

            return await executeAsync(scope);
        }
    }
}
