// Filename: ChallengesDbContextFactory.cs
// Date Created: 2019-05-03
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

namespace eDoxa.Arena.Challenges.Infrastructure.Factories
{
    internal sealed class ChallengesDbContextFactory : DesignTimeDbContextFactory<ChallengesDbContext>
    {
        protected override string BasePath => Path.Combine(Directory.GetCurrentDirectory(), "../eDoxa.Challenges.Api");

        protected override Assembly MigrationsAssembly => Assembly.GetAssembly(typeof(ChallengesDbContextFactory));

        [NotNull]
        public override ChallengesDbContext CreateDbContext(string[] args)
        {
            return new ChallengesDbContext(Options);
        }
    }
}