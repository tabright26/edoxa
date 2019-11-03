// Filename: IRedirectService.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public interface IRedirectService
    {
        string RedirectToWebSpaProxy(string url = "/");

        string RedirectToAuthority(string url = "/");
    }
}
