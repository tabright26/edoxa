// Filename: CashierDbContextFactory.cs
// Date Created: 2019-06-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.IO;
using System.Reflection;

using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Factories;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Migrations
{
    internal sealed class CashierDbContextFactory : DesignTimeDbContextFactory<CashierDbContext>
    {
        protected override string BasePath => Directory.GetCurrentDirectory();

        protected override Assembly MigrationsAssembly => Assembly.GetAssembly(typeof(Startup));

        
        public override CashierDbContext CreateDbContext(string[] args)
        {
            return new CashierDbContext(Options);
        }
    }
}
