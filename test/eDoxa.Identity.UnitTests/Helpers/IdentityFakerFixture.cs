﻿// Filename: IdentityFakerFixture.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;

using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Storage.Azure.File;
using eDoxa.Storage.Azure.File.Extensions;

using Microsoft.Extensions.Configuration;

namespace eDoxa.Identity.UnitTests.Helpers
{
    public sealed class IdentityFakerFixture
    {
        public IdentityFakerFixture()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            AzureFileStorageExtensions.ConfigureAzureStorageCredentials(configuration.GetSection("AzureFileStorage"));

            FileStorage = new IdentityFileStorage(new AzureFileStorage());
            TestFileStorage = new IdentityTestFileStorage(new AzureFileStorage());
        }

        public IdentityFileStorage FileStorage { get; }

        public IdentityTestFileStorage TestFileStorage { get; }
    }
}
