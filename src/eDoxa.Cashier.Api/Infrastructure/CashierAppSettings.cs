// Filename: CashierAppSettings.cs
// Date Created: 2019-07-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Cashier.Api.Infrastructure
{
    public class CashierAppSettings : IHasAzureKeyVaultAppSettings,
                                      IHasApiResourceAppSettings,
                                      IHasServiceBusAppSettings,
                                      IHasConnectionStringsAppSettings
    {
        [Required]
        public HealthChecksOptions HealthChecks { get; set; }

        public bool SwaggerEnabled { get; set; }

        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public AzureKeyVaultOptions AzureKeyVault { get; set; }

        [Required]
        public ConnectionStrings ConnectionStrings { get; set; }

        public bool AzureServiceBusEnabled { get; set; }

        [Required]
        public ServiceBusOptions ServiceBus { get; set; }
    }

    public class HealthChecksOptions
    {
        [Required]
        public string PaymentUrl { get; set; }
    }
}
