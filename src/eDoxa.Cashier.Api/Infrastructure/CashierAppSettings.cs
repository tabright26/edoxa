// Filename: CashierAppSettings.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Grpc.Protos.Cashier.Options;
using eDoxa.Seedwork.Application.Options;

namespace eDoxa.Cashier.Api.Infrastructure
{
    public class CashierAppSettings
    {
        [Required]
        public AuthorityOptions Authority { get; set; }

        public IntegrationEventOptions IntegrationEvent { get; set; }
    }
}
