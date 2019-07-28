// Filename: WebHostExtensions.cs
// Date Created: 2019-07-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Testing.Extensions
{
    public static class WebHostExtensions
    {
        public static void UsingScope(this IWebHost host, Action<IServiceScope> execute)
        {
            using (var scope = host.CreateScope())
            {
                execute(scope);
            }
        }

        public static async Task UsingScopeAsync(this IWebHost host, Func<IServiceScope, Task> executeAsync)
        {
            using (var scope = host.CreateScope())
            {
                await executeAsync(scope);
            }
        }

        
        public static async Task<TResult> UsingScopeAsync<TResult>(this IWebHost host, Func<IServiceScope, Task<TResult>> executeAsync)
        {
            using (var scope = host.CreateScope())
            {
                return await executeAsync(scope);
            }
        }

        internal static IServiceScope CreateScope(this IWebHost host)
        {
            return host.Services?.CreateScope() ?? throw new InvalidOperationException();
        }
    }
}
