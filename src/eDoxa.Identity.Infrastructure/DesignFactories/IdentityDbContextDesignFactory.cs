// Filename: IdentityDbContextDesignFactory.cs
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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eDoxa.Identity.Infrastructure.DesignFactories
{
    internal sealed class IdentityDbContextDesignFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<IdentityDbContext>().UseSqlServer(
                "Server=127.0.0.1,1433;Database=eDoxa.Services.Identity;User Id=sa;Password=fnU3Www9TnBDp3MA;",
                options =>
                {
                    options.MigrationsAssembly(Assembly.GetAssembly(typeof(IdentityDbContextDesignFactory)).GetName().Name);
                    options.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                }
            );

            return new IdentityDbContext(builder.Options);
        }
    }
}