// Filename: IdentityFakerFixture.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;

using eDoxa.Storage.Azure.File.Extensions;

using Microsoft.Extensions.Configuration;

namespace eDoxa.Payment.UnitTests.TestHelpers
{
    public sealed class TestDataFixture
    {
        public TestDataFixture()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            AzureFileStorageExtensions.ConfigureAzureStorageCredentials(configuration.GetSection("AzureFileStorage"));
        }
    }
}
