﻿// Filename: ConfigurationDbContextDesignFactory.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.IO;
using System.Reflection;

using eDoxa.Seedwork.Infrastructure.Factories;

using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;

namespace eDoxa.Identity.Infrastructure.Factories
{
    internal sealed class ConfigurationDbContextFactory : DesignTimeDbContextFactory<ConfigurationDbContext>
    {
        protected override string BasePath
        {
            get
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "../eDoxa.Identity");
            }
        }

        protected override Assembly MigrationsAssembly
        {
            get
            {
                return Assembly.GetAssembly(typeof(ConfigurationDbContextFactory));
            }
        }

        public override ConfigurationDbContext CreateDbContext(string[] args)
        {
            return new ConfigurationDbContext(Options, new ConfigurationStoreOptions());
        }
    }
}