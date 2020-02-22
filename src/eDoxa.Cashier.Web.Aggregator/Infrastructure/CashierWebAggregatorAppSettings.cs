// Filename: CashierWebAggregatorAppSettings.cs
// Date Created: 2019-12-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.Options;

namespace eDoxa.Cashier.Web.Aggregator.Infrastructure
{
    public class CashierWebAggregatorAppSettings
    {
        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public ServiceOptions Service { get; set; }

        [Required]
        public GrpcOptions Grpc { get; set; }
    }
}
