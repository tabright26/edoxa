// Filename: IRedirectService.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
namespace eDoxa.Notifications.Domain.Services
{
    public interface IRedirectService
    {
        string RedirectToWebSpa(string url = "/");
    }
}
