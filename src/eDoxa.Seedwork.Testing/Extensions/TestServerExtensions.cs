// Filename: TestServerExtensions.cs
// Date Created: 2019-06-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Testing.Extensions
{
    public static class TestServerExtensions
    {
        public static TDbContext MigrateDbContext<TDbContext>(this TestServer testServer)
        where TDbContext : DbContext
        {
            var scope = testServer.Host.Services.CreateScope();

            var provider = scope.ServiceProvider;

            var dbContext = provider.GetService<TDbContext>();

            dbContext.Database.Migrate();

            return dbContext;
        }
    }
}
