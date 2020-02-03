// Filename: TestMockFixture.cs
// Date Created: 2020-02-02
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Domain.Services;

using Moq;

namespace eDoxa.Challenges.TestHelper.Fixtures
{
    public sealed class TestMockFixture
    {
        public TestMockFixture()
        {
            ChallengeQuery = new Mock<IChallengeQuery>();
            MatchQuery = new Mock<IMatchQuery>();
            ParticipantQuery = new Mock<IParticipantQuery>();
            ChallengeRepository = new Mock<IChallengeRepository>();
            ChallengeService = new Mock<IChallengeService>();
        }

        public Mock<IChallengeQuery> ChallengeQuery { get; }

        public Mock<IMatchQuery> MatchQuery { get; }

        public Mock<IParticipantQuery> ParticipantQuery { get; }

        public Mock<IChallengeRepository> ChallengeRepository { get; }

        public Mock<IChallengeService> ChallengeService { get; }
    }
}
