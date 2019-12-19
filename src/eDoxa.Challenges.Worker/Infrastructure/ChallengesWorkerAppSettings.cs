// Filename: CashierAppSettings.cs
// Date Created: 2019-12-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

namespace eDoxa.Challenges.Worker.Infrastructure
{
    public class ChallengesWorkerAppSettings
    {
        [Required]
        public EndpointsOptions Endpoints { get; set; }
    }

    public sealed class EndpointsOptions
    {
        [Required]
        public string ChallengesUrl { get; set; }

        [Required]
        public string GamesUrl { get; set; }
    }
}
