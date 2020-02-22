// Filename: CashierAppSettings.cs
// Date Created: 2019-12-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.Options;

namespace eDoxa.Challenges.Workers.Infrastructure
{
    public class ChallengesWorkersAppSettings
    {
        [Required]
        public ServiceOptions Service { get; set; }

        [Required]
        public GrpcOptions Grpc { get; set; }
    }
}
