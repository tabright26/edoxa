// Filename: RedirectService.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Notifications.Api.Infrastructure;
using eDoxa.Notifications.Domain.Services;

using Microsoft.Extensions.Options;

namespace eDoxa.Notifications.Api.Application.Services
{
    public class RedirectService : IRedirectService
    {
        private readonly IOptions<NotificationsAppSettings> _optionsSnapshot;

        public RedirectService(IOptionsSnapshot<NotificationsAppSettings> optionsSnapshot)
        {
            _optionsSnapshot = optionsSnapshot;
        }

        private NotificationsAppSettings AppSettings => _optionsSnapshot.Value;

        public string RedirectToWebSpa(string url = "/")
        {
            return $"{AppSettings.WebSpaUrl}{url}";
        }

        public string RedirectToAuthority(string url = "/")
        {
            return $"{AppSettings.Authority}{url}";
        }
    }
}
