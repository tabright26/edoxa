// Filename: IdentityDbContextData.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Seedwork.Infrastructure.Abstractions;

namespace eDoxa.Identity.IntegrationTests
{
    internal sealed class ScenarioDbContextData : IDbContextData
    {
        public Task SeedAsync()
        {
            return Task.CompletedTask;
        }
    }
}
