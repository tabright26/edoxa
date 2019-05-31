// Filename: ServiceCollectionExtensionsTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

#region Directives

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using StackExchange.Redis;

#endregion

namespace eDoxa.Seedwork.Tests.Security
{
    [TestClass]
    public sealed class DataProtectionTest
    {
        [Ignore]
        public void AddDataProtection_PersistKeysToRedis_ShouldBeValidRedisConnectionMultiplexer()
        {
            // Arrange
            const string redisConnection = "localhost:6379";

            var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnection);

            // Act
            var services = new ServiceCollection();
            services.AddDataProtection().PersistKeysToRedis(connectionMultiplexer, "DataProtection-Keys");
            var provider = services.BuildServiceProvider();
            var dataProtector = provider.GetDataProtector("sample-purpose");
            var protect = dataProtector.Protect("Hello world!");

            // Assert
            Assert.IsNotNull(connectionMultiplexer);
            Assert.AreEqual(protect.Length, 134);
        }
    }
}
