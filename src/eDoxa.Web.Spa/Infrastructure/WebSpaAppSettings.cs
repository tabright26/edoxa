﻿// Filename: WebSpaAppSettings.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.AppSettings;
using eDoxa.Seedwork.Application.AppSettings.Options;

namespace eDoxa.Web.Spa.Infrastructure
{
    public class WebSpaAppSettings : IHasAuthorityAppSettings, IHasEndpointsAppSettings<AuthorityEndpointsOptions>
    {
        [Required]
        public string Authority { get; set; }

        public string WebSpaClientUrl { get; set; }

        [Required]
        public AuthorityEndpointsOptions Endpoints { get; set; }
    }
}
