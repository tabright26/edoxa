// Filename: TestDataFixture.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage;
using eDoxa.Storage.Azure.File;
using eDoxa.Storage.Azure.File.Extensions;

using Microsoft.Extensions.Configuration;

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers
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

            FileStorage = new ArenaChallengeTestFileStorage(new AzureFileStorage(), new ChallengeFakerFactory());
        }

        public ArenaChallengeTestFileStorage FileStorage { get; }
    }
}
