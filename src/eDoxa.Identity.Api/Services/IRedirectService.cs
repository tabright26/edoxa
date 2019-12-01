// Filename: IRedirectService.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
namespace eDoxa.Identity.Api.Services
{
    public interface IRedirectService
    {
        string RedirectToWebSpa(string url = "/");

        string RedirectToAuthority(string url = "/");
    }
}
