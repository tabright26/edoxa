// Filename: WebApplicationFactory.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Testing;

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers
{
    internal sealed class WebApplicationFactory<TStartup> : WebApplicationFactory<ChallengesDbContext, TStartup, Program>
    where TStartup : TestStartup
    {
    }
}
