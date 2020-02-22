// Filename: IdentityAppSettings.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Grpc.Protos.Identity.Options;
using eDoxa.Seedwork.Application.Options;

namespace eDoxa.Identity.Api.Infrastructure
{
    public class IdentityAppSettings
    {
        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public ClientOptions Client { get; set; }

        public ServiceOptions Service { get; set; }

        public AggregatorOptions Aggregator { get; set; }

        [Required]
        public AdministratorOptions Administrator { get; set; }
    }
}
