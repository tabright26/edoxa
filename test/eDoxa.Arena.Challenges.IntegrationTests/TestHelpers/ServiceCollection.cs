// Filename: ServiceCollection.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Arena.Challenges.IntegrationTests.TestHelpers
{
    [CollectionDefinition(nameof(ServiceCollection))]
    public class ServiceCollection : IClassFixture<ArenaChallengeApiFactory>, ICollectionFixture<TestDataFixture>
    {
    }
}
