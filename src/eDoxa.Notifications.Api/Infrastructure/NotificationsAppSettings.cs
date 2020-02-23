// Filename: NotificationsAppSettings.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.Options;

namespace eDoxa.Notifications.Api.Infrastructure
{
    public class NotificationsAppSettings
    {
        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public ClientOptions Client { get; set; }
    }
}
