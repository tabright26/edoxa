// Filename: RepositoryCollection.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Helpers
{
    [CollectionDefinition(nameof(RepositoryCollection))]
    public class RepositoryCollection : IClassFixture<CashierApiFactory>, ICollectionFixture<TestDataFixture>

    {
    }
}
