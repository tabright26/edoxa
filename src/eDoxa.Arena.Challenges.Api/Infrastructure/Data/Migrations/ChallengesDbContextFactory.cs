// Filename: ChallengesDbContextFactory.cs
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

using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Factories;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Migrations
{
    internal sealed class ChallengesDbContextFactory : DesignTimeDbContextFactory<ChallengesDbContext>
    {
        protected override string BasePath => Directory.GetCurrentDirectory();

        protected override Assembly MigrationsAssembly => Assembly.GetAssembly(typeof(Startup));

        [NotNull]
        public override ChallengesDbContext CreateDbContext(string[] args)
        {
            return new ChallengesDbContext(Options, new FakeMediator());
        }
    }
}
