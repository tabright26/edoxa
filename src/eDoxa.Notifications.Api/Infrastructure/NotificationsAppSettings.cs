// Filename: NotificationsAppSettings.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.AppSettings;
using eDoxa.Seedwork.Application.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Notifications.Api.Infrastructure
{
    public class NotificationsAppSettings : IHasApiResourceAppSettings<AuthorityEndpointsOptions>
    {
        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public string Authority { get; set; }
        
        [Required]
        public string WebSpaUrl { get; set; }

        [Required]
        public AuthorityEndpointsOptions Endpoints { get; set; }
    }
}
