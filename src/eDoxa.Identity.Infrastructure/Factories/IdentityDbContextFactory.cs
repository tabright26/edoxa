// Filename: IdentityDbContextFactory.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.IO;
using System.Reflection;

using eDoxa.Seedwork.Infrastructure.Factories;

using JetBrains.Annotations;

namespace eDoxa.Identity.Infrastructure.Factories
{
    internal sealed class IdentityDbContextFactory : DesignTimeDbContextFactory<IdentityDbContext>
    {
        protected override string BasePath => Path.Combine(Directory.GetCurrentDirectory(), "../eDoxa.Identity.Api");

        protected override Assembly MigrationsAssembly => Assembly.GetAssembly(typeof(IdentityDbContextFactory));

        [NotNull]
        public override IdentityDbContext CreateDbContext(string[] args)
        {
            return new IdentityDbContext(Options);
        }
    }
}
