// Filename: AppSettingsTest.cs
// Date Created: 2019-07-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;
using System.IO;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;
using eDoxa.Seedwork.Monitoring.Extensions;

using FluentAssertions;

using IdentityServer4.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.Monitoring
{
    [TestClass]
    public sealed class AppSettingsTest
    {
        public IConfiguration Configuration => new ConfigurationBuilder().AddJsonFile(Path.Combine("Monitoring", "appsettings.json")).Build();

        [TestMethod]
        public void AddAppSettings_GetService_ShouldNotBeNull()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAppSettings<MockAppSettings>(Configuration);
            var provider = services.BuildServiceProvider();

            // Act
            var appSettings = provider.GetService<IOptions<MockAppSettings>>();

            // Assert
            appSettings.Should().NotBeNull();
        }

        [TestMethod]
        public void AppSettings_IsValid_ShouldBeFalse()
        {
            // Arrange
            var appSettings = new MockAppSettings();

            // Act
            var isValid = appSettings.IsValid();

            // Assert
            isValid.Should().BeFalse();
        }

        private sealed class MockAppSettings : IHasApiResourceAppSettings, IHasServiceBusAppSettings, IHasAzureKubernetesServiceAppSettings
        {
            [Required]
            public AuthorityOptions Authority { get; set; }

            public bool SwaggerEnabled { get; set; }

            [Required]
            public ApiResource ApiResource { get; set; }

            public bool AzureKubernetesServiceEnabled { get; set; }

            public bool AzureServiceBusEnabled { get; set; }

            [Required]
            public ServiceBusOptions ServiceBus { get; set; }
        }
    }
}
