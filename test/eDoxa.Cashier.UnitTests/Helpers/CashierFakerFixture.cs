// Filename: CashierFakerFixture.cs
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

namespace eDoxa.Cashier.UnitTests.Helpers
{
    public sealed class CashierFakerFixture
    {
        public CashierFakerFixture()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            AzureFileStorageExtensions.ConfigureAzureStorageCredentials(configuration.GetSection("AzureFileStorage"));

            ChallengeFactory = new ChallengeFakerFactory();
            TransactionFactory = new TransactionFakerFactory();
            AccountFactory = new AccountFakerFactory();
            FileStorage = new CashierFileStorage(new AzureFileStorage());
            TestFileStorage = new CashierTestFileStorage(new AzureFileStorage());
        }

        public IChallengeFakerFactory ChallengeFactory { get; }

        public ITransactionFakerFactory TransactionFactory { get; }

        public IAccountFakerFactory AccountFactory { get; }

        public CashierFileStorage FileStorage { get; }

        public CashierTestFileStorage TestFileStorage { get; }
    }
}
