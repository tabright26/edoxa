#region Terms and Conditions

// Filename: ServiceCollectionExtensionsTest.cs
// Date Created: 2019-01-31
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

#endregion

#region Directives

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using StackExchange.Redis;

#endregion

namespace eDoxa.Security.Tests.Extensions
{
    [TestClass]
    public sealed class ServiceCollectionExtensionsTest
    {
        [TestMethod]
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