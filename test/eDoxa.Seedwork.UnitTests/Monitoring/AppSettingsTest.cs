// Filename: AppSettingsTest.cs
// Date Created: 2019-09-16
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

using Xunit;

namespace eDoxa.Seedwork.UnitTests.Monitoring
{
    public sealed class AppSettingsTest
    {
        public IConfiguration Configuration => new ConfigurationBuilder().AddJsonFile(Path.Combine("Monitoring", "appsettings.json")).Build();

        private sealed class MockAppSettings : IHasApiResourceAppSettings, IHasAzureKubernetesServiceAppSettings
        {
            [Required]
            public string Authority { get; set; }

            [Required]
            public ApiResource ApiResource { get; set; }

            [Required]
            public bool AzureKubernetesServiceEnabled { get; set; }

            [Required]
            public AuthorityEndpointsOptions Endpoints { get; set; }
        }

        [Fact]
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

        [Fact]
        public void AppSettings_IsValid_ShouldBeFalse()
        {
            // Arrange
            var appSettings = new MockAppSettings();

            // Act
            var isValid = appSettings.IsValid();

            // Assert
            isValid.Should().BeFalse();
        }
    }
}
