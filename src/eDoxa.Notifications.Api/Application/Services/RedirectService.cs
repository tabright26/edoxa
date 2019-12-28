﻿// Filename: RedirectService.cs
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
        public RedirectService(IOptions<NotificationsAppSettings> optionsAccessor)
        {
            AppSettings = optionsAccessor.Value;
        }

        private NotificationsAppSettings AppSettings { get; }

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