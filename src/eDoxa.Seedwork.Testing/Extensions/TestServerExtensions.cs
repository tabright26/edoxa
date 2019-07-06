// Filename: TestServerExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Infrastructure.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Testing.Extensions
{
    public static class TestServerExtensions
    {
        public static void EnsureCreatedDbContext<TDbContext>(this TestServer testServer)
        where TDbContext : DbContext
        {
            testServer.UsingScope(scope => scope?.GetService<TDbContext>().Database?.EnsureCreated());
        }

        public static void MigrateDbContext<TDbContext>(this TestServer testServer)
        where TDbContext : DbContext
        {
            testServer.UsingScope(scope => scope?.GetService<TDbContext>().Database?.Migrate());
        }

        public static async Task CleanupDbContextAsync(this TestServer testServer)
        {
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var dbContextData = scope?.GetService<IDbContextData>() ?? throw new InvalidOperationException();

                    await dbContextData.CleanupAsync();
                }
            );
        }

        public static void UsingScope(this TestServer testServer, Action<IServiceScope> execute)
        {
            testServer.Host.UsingScope(execute);
        }

        public static async Task UsingScopeAsync(this TestServer testServer, Func<IServiceScope, Task> executeAsync)
        {
            await testServer.Host.UsingScopeAsync(executeAsync);
        }

        [ItemNotNull]
        public static async Task<TResult> UsingScopeAsync<TResult>(this TestServer testServer, Func<IServiceScope, Task<TResult>> executeAsync)
        {
            return await testServer.Host.UsingScopeAsync(executeAsync);
        }
    }
}
