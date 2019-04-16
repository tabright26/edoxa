// Filename: CashierDbContextFactory.cs
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

using JetBrains.Annotations;

namespace eDoxa.Cashier.Infrastructure.Factories
{
    internal sealed class CashierDbContextFactory : DesignTimeDbContextFactory<CashierDbContext>
    {
        protected override string BasePath
        {
            get
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "../eDoxa.Cashier.Api");
            }
        }

        protected override Assembly MigrationsAssembly
        {
            get
            {
                return Assembly.GetAssembly(typeof(CashierDbContextFactory));
            }
        }

        [NotNull]
        public override CashierDbContext CreateDbContext(string[] args)
        {
            return new CashierDbContext(Options);
        }
    }
}