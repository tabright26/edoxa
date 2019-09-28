// Filename: UnitTest.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers
{
    public abstract class UnitTest : IClassFixture<ChallengeFakerFixture>
    {
        protected UnitTest(ChallengeFakerFixture challengeFaker)
        {
            ChallengeFaker = challengeFaker;
        }

        protected ChallengeFakerFixture ChallengeFaker { get; }
    }
}
