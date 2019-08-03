// Filename: PaymentAppSettings.cs
// Date Created: 2019-07-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Payment.Api.Infrastructure
{
    public class PaymentAppSettings : IHasAzureKeyVaultAppSettings, IHasServiceBusAppSettings
    {
        [Required]
        public AzureKeyVaultOptions AzureKeyVault { get; set; }

        public bool AzureServiceBusEnabled { get; set; }
    }
}
