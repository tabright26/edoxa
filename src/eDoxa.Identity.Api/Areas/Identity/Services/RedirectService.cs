// Filename: RedirectService.cs
// Date Created: 2019-08-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Infrastructure;

using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public class RedirectService : IRedirectService
    {
        public RedirectService(IOptions<IdentityAppSettings> optionsAccessor)
        {
            AppSettings = optionsAccessor.Value;
        }

        private IdentityAppSettings AppSettings { get; }

        public string RedirectToWebSpaProxy(string url = "/")
        {
            return $"{AppSettings.WebSpaProxyUrl}{url}";
        }

        public string RedirectToAuthority(string url = "/")
        {
            return $"{AppSettings.Authority}{url}";
        }
    }
}
