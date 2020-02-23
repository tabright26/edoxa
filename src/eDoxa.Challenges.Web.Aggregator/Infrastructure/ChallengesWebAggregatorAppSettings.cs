// Filename: ChallengesWebAggregatorAppSettings.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.Options;

namespace eDoxa.Challenges.Web.Aggregator.Infrastructure
{
    public class ChallengesWebAggregatorAppSettings
    {
        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public ServiceOptions Service { get; set; }

        [Required]
        public GrpcOptions Grpc { get; set; }
    }
}
