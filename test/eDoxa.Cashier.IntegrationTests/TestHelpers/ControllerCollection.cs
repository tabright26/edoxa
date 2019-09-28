// Filename: ControllerCollection.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.TestHelpers
{
    [CollectionDefinition(nameof(ControllerCollection))]
    public class ControllerCollection : IClassFixture<CashierApiFactory>, ICollectionFixture<TestDataFixture>
    {
    }
}
