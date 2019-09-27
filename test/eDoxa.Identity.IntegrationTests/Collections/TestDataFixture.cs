﻿// Filename: AssemblyInitializeFixture.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;

using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Storage.Azure.File;
using eDoxa.Storage.Azure.File.Extensions;

using Microsoft.Extensions.Configuration;

namespace eDoxa.Identity.IntegrationTests.Collections
{
    public class TestDataFixture
    {
        public TestDataFixture()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            AzureFileStorageExtensions.ConfigureAzureStorageCredentials(configuration.GetSection("AzureFileStorage"));

            TestData = new IdentityTestFileStorage(new AzureFileStorage());
        }

        public IIdentityTestFileStorage TestData { get; }
    }
}
