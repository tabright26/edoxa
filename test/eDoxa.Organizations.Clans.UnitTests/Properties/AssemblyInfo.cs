// Filename: AssemblyInfo.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;

using eDoxa.Storage.Azure.File.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if DEBUG
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel, Workers = 0)]
#else
[assembly: DoNotParallelize]
#endif

namespace eDoxa.Organizations.Clans.UnitTests.Properties
{
    [TestClass]
    public sealed class AssemblyInitializeTest
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext _)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            AzureFileStorageExtensions.ConfigureAzureStorageCredentials(configuration.GetSection("AzureFileStorage"));
        }
    }
}
