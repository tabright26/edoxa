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
        private readonly IOptions<NotificationsAppSettings> _options;

        public RedirectService(IOptionsSnapshot<NotificationsAppSettings> options)
        {
            _options = options;
        }

        private NotificationsAppSettings Options => _options.Value;

        public string RedirectToWebSpa(string url = "/")
        {
            return $"{Options.Client.Web.Endpoints.SpaUrl}{url}";
        }
    }
}
