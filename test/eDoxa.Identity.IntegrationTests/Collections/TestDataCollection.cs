// Filename: AzureFileStorageCollection.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Storage.Azure.File;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Collections
{
    [CollectionDefinition(nameof(TestDataFixture.TestData))]
    public class TestDataCollection : ICollectionFixture<TestDataFixture>
    {
    }
}
