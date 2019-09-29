// Filename: TestDataFixture.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Storage.Azure.File;
using eDoxa.Storage.Azure.File.Extensions;

using Microsoft.Extensions.Configuration;

namespace eDoxa.Cashier.UnitTests.TestHelpers
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

            FakerFactory = new FakerFactory();
            FileStorage = new CashierFileStorage(new AzureFileStorage());
            TestFileStorage = new CashierTestFileStorage(new AzureFileStorage());
        }

        public IFakerFactory FakerFactory { get; }

        public ICashierFileStorage FileStorage { get; }

        public ICashierTestFileStorage TestFileStorage { get; }
    }
}
