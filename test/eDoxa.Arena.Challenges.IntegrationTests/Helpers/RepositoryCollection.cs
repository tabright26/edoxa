// Filename: RepositoryCollection.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers
{
    [CollectionDefinition(nameof(RepositoryCollection))]
    public class RepositoryCollection : IClassFixture<ArenaChallengeApiFactory>, ICollectionFixture<TestDataFixture>
    {
    }
}
