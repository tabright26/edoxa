// Filename: WebSpaAppSettings.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.Options;

namespace eDoxa.Web.Spa.Infrastructure
{
    public class WebSpaAppSettings
    {
        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public GatewayOptions Gateway { get; set; }

        public ClientOptions Client { get; set; }
    }
}
