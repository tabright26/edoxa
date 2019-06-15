// Filename: TestServerExtensions.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Testing.TestServer.Extensions
{
    public static class TestServerExtensions
    {
        public static TDbContext MigrateDbContext<TDbContext>(this Microsoft.AspNetCore.TestHost.TestServer testServer)
        where TDbContext : DbContext
        {
            var scope = testServer.Host.Services.CreateScope();

            var provider = scope.ServiceProvider;

            var dbContext = provider.GetService<TDbContext>();

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        public static IMapper ResolveMapper(this Microsoft.AspNetCore.TestHost.TestServer testServer)
        {
            var scope = testServer.Host.Services.CreateScope();

            var provider = scope.ServiceProvider;

            return provider.GetService<IMapper>();
        }
    }
}
