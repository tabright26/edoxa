// Filename: TestServerExtensions.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Testing.Extensions
{
    public static class TestServerExtensions
    {
        public static async Task UsingScopeAsync(this TestServer testServer, Func<IServiceScope, Task> serviceScope)
        {
            using (var scope = testServer.Host.Services.CreateScope())
            {
                await serviceScope(scope);
            }
        }

        [ItemNotNull]
        public static async Task<TResult> UsingScopeAsync<TResult>(this TestServer testServer, Func<IServiceScope, Task<TResult>> serviceScope)
        {
            using (var scope = testServer.Host.Services.CreateScope())
            {
                return await serviceScope(scope);
            }
        }
    }
}
