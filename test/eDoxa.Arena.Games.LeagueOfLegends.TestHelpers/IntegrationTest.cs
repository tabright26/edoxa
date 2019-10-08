﻿// Filename: IntegrationTest.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.LeagueOfLegends.TestHelpers.Fixtures;

using Xunit;

namespace eDoxa.Arena.Games.LeagueOfLegends.TestHelpers
{
    public abstract class IntegrationTest : IClassFixture<TestApiFixture>
    {
        protected IntegrationTest(TestApiFixture testApi)
        {
            TestApi = testApi;
        }

        protected TestApiFixture TestApi { get; }
    }
}