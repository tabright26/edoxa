// Filename: ConfigurationDbContextDesignFactory.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eDoxa.Identity.Infrastructure.DesignFactories
{
    internal sealed class ConfigurationDbContextDesignFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ConfigurationDbContext>().UseSqlServer(
                "Server=127.0.0.1,1433;Database=eDoxa.Services.Identity;User Id=sa;Password=fnU3Www9TnBDp3MA;",
                options =>
                {
                    options.MigrationsAssembly(Assembly.GetAssembly(typeof(ConfigurationDbContextDesignFactory)).GetName().Name);
                    options.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                }
            );

            return new ConfigurationDbContext(builder.Options, new ConfigurationStoreOptions());
        }
    }
}