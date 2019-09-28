// Filename: AzureFileStorageCollection.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Helpers
{
    [CollectionDefinition(nameof(ControllerCollection))]
    public class ControllerCollection : IClassFixture<IdentityApiFactory>, ICollectionFixture<TestDataFixture>
    {
    }
}
