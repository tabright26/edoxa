// Filename: ServiceBusOptions.cs
// Date Created: 2019-07-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace eDoxa.Seedwork.Monitoring.AppSettings.Options
{
    public class ServiceBusOptions
    {
        [Required]
        public string HostName { get; set; }

        public int? Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int? RetryCount { get; set; }

        [Required]
        public string SubscriptionName { get; set; }
    }
}
