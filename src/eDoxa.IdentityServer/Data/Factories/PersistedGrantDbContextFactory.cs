﻿// Filename: PersistedGrantDbContextDesignFactory.cs
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

using JetBrains.Annotations;

namespace eDoxa.IdentityServer.Data.Factories
{
    internal sealed class PersistedGrantDbContextFactory : DesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        protected override string BasePath
        {
            get
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "../eDoxa.IdentityServer");
            }
        }

        protected override Assembly MigrationsAssembly
        {
            get
            {
                return Assembly.GetAssembly(typeof(PersistedGrantDbContextFactory));
            }
        }

        [NotNull]
        public override PersistedGrantDbContext CreateDbContext(string[] args)
        {
            return new PersistedGrantDbContext(Options, new OperationalStoreOptions());
        }
    }
}